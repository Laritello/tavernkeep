using Tavernkeep.Application.Interfaces;
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
		ICharacterService characterService,
		INotificationService notificationService,
		IDiceService diceService
		) : IEncounterService
	{
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
			return await encounterRepository.GetAllEncountersAsync(cancellationToken);
		}

		public async Task<Encounter> GetEncounterAsync(Guid encounterId, CancellationToken cancellationToken)
		{
			return await encounterRepository.GetFullEncounterAsync(encounterId, cancellationToken) 
				?? throw new BusinessLogicException("Encounter not found");
		}

		public async Task UpdateEncounterStatusAsync(Guid encounterId, EncounterStatus status, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.FindAsync(encounterId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Encounter not found.");

			if (status.IsEarlierThan(encounter.Status))
				throw new BusinessLogicException("Can't change encounter status to the previous status.");

			encounter.Status = status;

			encounterRepository.Save(encounter);
			await encounterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new EncounterUpdatedNotification(encounter), cancellationToken);

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
				default:
					throw new NotImplementedException();
			}
		}

		public async Task RemoveParticipantAsync(Guid encounterId, Guid participantId, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.FindAsync(new EncounterFullSpecification(encounterId), cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Encounter not found.");

			encounter.RemoveParticipant(encounter.Participants.First(x => x.Id == participantId));
			
			encounterRepository.Save(encounter);
			await encounterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new EncounterUpdatedNotification(encounter), cancellationToken);
		}

		private async Task AddCharacterParticipantAsync(Encounter encounter, Guid characterId, CancellationToken cancellationToken)
		{
			var character = await characterService.GetCharacterAsync(characterId, cancellationToken)
				?? throw new BusinessLogicException("Character not found");

			CharacterEncounterParticipant participant = new()
			{
				Encounter = encounter,
				Character = character,
			};

			encounter.AddParticipant(participant);

			encounterRepository.Save(encounter);
			await encounterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new EncounterUpdatedNotification(encounter), cancellationToken);
		}

		public async Task UpdateParticipantsOrdinalAsync(Guid encounterId, IList<Guid> ordinals, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.FindAsync(new EncounterFullSpecification(encounterId), cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Encounter not found.");

			foreach(var participant in encounter.Participants)
			{
				participant.Ordinal = ordinals.IndexOf(participant.Id);
			}

			encounterRepository.Save(encounter);
			await encounterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new EncounterUpdatedNotification(encounter), cancellationToken);
		}

		public async Task RollInitiative(Guid encounterId, Guid userId, bool npcOnly, CancellationToken cancellationToken)
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

			encounterRepository.Save(encounter);
			await encounterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new EncounterUpdatedNotification(encounter), cancellationToken);
		}

		public async Task RollInitiativeForParticipant(Guid encounterId, Guid userId, Guid participantId, string skillName, CancellationToken cancellationToken)
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

			encounterRepository.Save(encounter);
			await encounterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new EncounterUpdatedNotification(encounter), cancellationToken);
		}

		private async Task RollInitiative(CharacterEncounterParticipant participant, Guid userId, CancellationToken cancellationToken, string skillName = "Perception")
		{
			var character = await characterService.RetrieveCharacterForAction(participant.Character.Id, userId, cancellationToken);

			var skill = character.Skills[skillName] ?? throw new BusinessLogicException("Character doesn't have specified skill");
			var roll = diceService.Roll(bonus: skill.Bonus);

			participant.Initiative = roll.Value;
		}
	}
}
