﻿using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterCreated;
using Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterLaunched;
using Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterUpdated;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters;
using Tavernkeep.Core.Entities.Encounters.Participants;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Extensions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Services;
using Tavernkeep.Core.Specifications.Encounters;

namespace Tavernkeep.Application.Services
{
	public class EncounterService(
		IEncounterRepository encounterRepository,
		ICreatureLibraryRepository creatureRepository,
		ICharacterService characterService,
		INotificationService notificationService,
		IDiceService diceService
		) : IEncounterService
	{
		#region Public API

		public async Task<Encounter> CreateEncounterAsync(string name, CancellationToken cancellationToken)
		{
			Encounter encounter = new(name, EncounterStatus.Draft);

			encounterRepository.Save(encounter);
			await encounterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new EncounterCreatedNotification(encounter), cancellationToken);

			return encounter;
		}

		public async Task<ICollection<Encounter>> GetAllEncountersAsync(CancellationToken cancellationToken)
		{
			var encounters = await encounterRepository.GetAllEncountersAsync(cancellationToken);

			foreach (var encounter in encounters)
			{
				foreach (var participant in encounter.Participants)
				{
					switch (participant)
					{
						case CharacterEncounterParticipant characterParticipant:
							characterParticipant.Character = await characterService.GetCharacterAsync(characterParticipant.CharacterId, cancellationToken);
							break;
						case CreatureEncounterParticipant creatureParticipant:
							creatureParticipant.Creature = await creatureRepository.GetCreatureAsync(creatureParticipant.CreatureId, cancellationToken);
							break;
					}
				}
			}

			return encounters;
		}

		public async Task<Encounter> GetEncounterAsync(Guid encounterId, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.GetFullEncounterAsync(encounterId, cancellationToken) 
				?? throw new BusinessLogicException("Encounter not found");

			foreach (var participant in encounter.Participants)
			{
				switch (participant)
				{
					case CharacterEncounterParticipant characterParticipant:
						characterParticipant.Character = await characterService.GetCharacterAsync(characterParticipant.CharacterId, cancellationToken);
						break;
					case CreatureEncounterParticipant creatureParticipant:
						creatureParticipant.Creature = await creatureRepository.GetCreatureAsync(creatureParticipant.CreatureId, cancellationToken);
						break;
				}
			}

			return encounter;
		}

		public async Task UpdateEncounterStatusAsync(Guid encounterId, EncounterStatus status, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.FindAsync(encounterId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Encounter not found.");

			if (status.IsEarlierThan(encounter.Status))
				throw new BusinessLogicException("Can't change encounter status to the previous status.");

			encounter.Status = status;

			await SaveEncounter(encounter, cancellationToken);

			if (encounter.Status == EncounterStatus.Initiative)
			{
				await notificationService.Publish(new EncounterLaunchedNotification(encounter), cancellationToken);
			}
		}

		public async Task AddParticipantAsync(Guid encounterId, EncounterParticipantType type, Guid entityId, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.FindAsync(new EncounterFullSpecification(encounterId), cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Encounter not found.");

			switch (type)
			{
				case EncounterParticipantType.Character:
					await AddCharacterParticipantAsync(encounter, entityId, cancellationToken);
					break;
				case EncounterParticipantType.Creature:
					await AddCreatureParticipantAsync(encounter, entityId, cancellationToken);
					break;
				default:
					throw new NotImplementedException();
			}

			await SaveEncounter(encounter, cancellationToken);
		}

		public async Task RemoveParticipantAsync(Guid encounterId, Guid participantId, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.FindAsync(new EncounterFullSpecification(encounterId), cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Encounter not found.");

			encounter.RemoveParticipant(encounter.Participants.First(x => x.Id == participantId));

			await SaveEncounter(encounter, cancellationToken);
		}

		public async Task UpdateParticipantsOrdinalAsync(Guid encounterId, IList<Guid> ordinals, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.FindAsync(new EncounterFullSpecification(encounterId), cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Encounter not found.");

			foreach(var participant in encounter.Participants)
			{
				participant.Ordinal = ordinals.IndexOf(participant.Id);
			}

			await SaveEncounter(encounter, cancellationToken);
		}

		public async Task RollInitiativeAsync(Guid encounterId, Guid userId, bool npcOnly, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.GetFullEncounterAsync(encounterId, cancellationToken) 
				?? throw new BusinessLogicException("Encounter not found");

			foreach (var participant in encounter.Participants)
			{
				switch (participant)
				{
					case CharacterEncounterParticipant characterParticipant:
						if (!npcOnly)
						{
							await RollInitiative(characterParticipant, userId, cancellationToken);
						}
						break;
				}
			}

			if (encounter.Status == EncounterStatus.Initiative)
			{
				encounter.OrderByInitiative();
			}

			await SaveEncounter(encounter, cancellationToken);
		}

		public async Task SetInitiativeForParticipantAsync(Guid encounterId, Guid userId, Guid participantId, int initiative, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.GetFullEncounterAsync(encounterId, cancellationToken)
				?? throw new BusinessLogicException("Encounter not found");
			var participant = encounter.Participants.First(x => x.Id == participantId)
				?? throw new BusinessLogicException("Participant not found.");

			participant.Initiative = initiative;

			if (encounter.Status == EncounterStatus.Initiative)
			{
				encounter.OrderByInitiative();
			}

			await SaveEncounter(encounter, cancellationToken);
		}

		public async Task RollInitiativeForParticipantAsync(Guid encounterId, Guid userId, Guid participantId, string skillName, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.GetFullEncounterAsync(encounterId, cancellationToken) 
				?? throw new BusinessLogicException("Encounter not found");
			var participant = encounter.Participants.First(x => x.Id == participantId)
				?? throw new BusinessLogicException("Participant not found.");

			switch (participant)
			{
				case CharacterEncounterParticipant characterParticipant:
					await RollInitiative(characterParticipant, userId, cancellationToken);
					break;
			}

			if (encounter.Status == EncounterStatus.Initiative)
			{
				encounter.OrderByInitiative();
			}

			await SaveEncounter(encounter, cancellationToken);
		}

		public async Task ClearInitiativeAsync(Guid encounterId, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.GetFullEncounterAsync(encounterId, cancellationToken)
				?? throw new BusinessLogicException("Encounter not found");

			foreach (var participant in encounter.Participants)
			{
				participant.Initiative = null;
			}

			await SaveEncounter(encounter, cancellationToken);
		}

		public async Task UpdateTurnAsync(Guid encounterId, bool moveForward, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.GetFullEncounterAsync(encounterId, cancellationToken)
				?? throw new BusinessLogicException("Encounter not found");

			if (moveForward)
			{
				encounter.NextTurn();
			}
			else
			{
				encounter.PreviousTurn();
			}

			await SaveEncounter(encounter, cancellationToken);
		}

		#endregion

		#region Private functions

		private async Task AddCharacterParticipantAsync(Encounter encounter, Guid characterId, CancellationToken cancellationToken)
		{
			var character = await characterService.GetCharacterAsync(characterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID not found");

			CharacterEncounterParticipant participant = new()
			{
				Encounter = encounter,
				Character = character,
			};

			encounter.AddParticipant(participant);
		}

		private async Task AddCreatureParticipantAsync(Encounter encounter, Guid creatureId, CancellationToken cancellationToken)
		{
			var creature = await creatureRepository.GetCreatureAsync(creatureId, cancellationToken)
				?? throw new BusinessLogicException("Creature with specified ID not found");

			CreatureEncounterParticipant participant = new()
			{
				Encounter = encounter,
				Creature = creature,
			};

			encounter.AddParticipant(participant);
		}

		private async Task RollInitiative(CharacterEncounterParticipant participant, Guid userId, CancellationToken cancellationToken, string skillName = "Perception")
		{
			var character = await characterService.RetrieveCharacterForAction(participant.Character.Id, userId, cancellationToken);

			var skill = character.Skills[skillName] ?? throw new BusinessLogicException("Character doesn't have specified skill");
			var roll = diceService.Roll(bonus: skill.Bonus);

			participant.Initiative = roll.Value;
		}

		private async Task SaveEncounter(Encounter encounter, CancellationToken cancellationToken)
		{
			encounterRepository.Save(encounter);
			await encounterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new EncounterUpdatedNotification(encounter), cancellationToken);
		}

		#endregion
	}
}
