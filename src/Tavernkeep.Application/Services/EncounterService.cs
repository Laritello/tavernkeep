using Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterCreated;
using Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterDeleted;
using Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterLaunched;
using Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterUpdated;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Extensions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Services;
using Tavernkeep.Core.Strategies.Encounters;

namespace Tavernkeep.Application.Services
{
	public class EncounterService(
		IEncounterRepository encounterRepository,
		INotificationService notificationService,
		IEncounterServiceStrategies strategies
		) : IEncounterService
	{
		#region Public API

		public async Task<Encounter> CreateEncounterAsync(string name, CancellationToken cancellationToken)
		{
			Encounter encounter = new(name, EncounterStatus.Draft)
			{
				Created = DateTimeOffset.UtcNow
			};

			encounterRepository.Save(encounter);
			await encounterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new EncounterCreatedNotification(encounter), cancellationToken);

			return encounter;
		}

		public async Task DeleteEncounterAsync(Guid encounterId, CancellationToken cancellationToken)
		{
			var encounter = await GetEncounterAsync(encounterId, cancellationToken);

			encounterRepository.Remove(encounter);
			await encounterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new EncounterDeletedNotification(encounter), cancellationToken);
		}

		public async Task<ICollection<Encounter>> GetAllEncountersAsync(CancellationToken cancellationToken)
		{
			var encounters = await encounterRepository.GetAllEncountersAsync(cancellationToken);

			foreach (var encounter in encounters)
			{
				foreach (var participant in encounter.Participants)
				{
					await strategies.FillParticipant[participant.Type].FillParticipantAsync(participant, cancellationToken);
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
				await strategies.FillParticipant[participant.Type].FillParticipantAsync(participant, cancellationToken);
			}

			return encounter;
		}

		public async Task EditEncounterStatusAsync(Guid encounterId, EncounterStatus status, CancellationToken cancellationToken)
		{
			var encounter = await GetEncounterAsync(encounterId, cancellationToken);

			if (status.IsEarlierThan(encounter.Status))
				throw new BusinessLogicException("Can't change encounter status to the previous status.");

			encounter.Status = status;

			await SaveEncounter(encounter, cancellationToken);

			if (encounter.Status == EncounterStatus.Active)
			{
				await notificationService.Publish(new EncounterLaunchedNotification(encounter), cancellationToken);
			}
		}

		public async Task AddParticipantAsync(Guid encounterId, EncounterParticipantType type, Guid entityId, CancellationToken cancellationToken)
		{
			var encounter = await GetEncounterAsync(encounterId, cancellationToken);

			await strategies.AddParticipant[type].AddParticipantAsync(encounter, entityId, cancellationToken);
			await SaveEncounter(encounter, cancellationToken);
		}

		public async Task DeleteParticipantAsync(Guid encounterId, Guid participantId, CancellationToken cancellationToken)
		{
			var encounter = await GetEncounterAsync(encounterId, cancellationToken);

			encounter.RemoveParticipant(encounter.Participants.First(x => x.Id == participantId));

			await SaveEncounter(encounter, cancellationToken);
		}

		public async Task EditParticipantsOrdinalAsync(Guid encounterId, IList<Guid> ordinals, CancellationToken cancellationToken)
		{
			var encounter = await GetEncounterAsync(encounterId, cancellationToken);

			foreach (var participant in encounter.Participants)
			{
				participant.Ordinal = ordinals.IndexOf(participant.Id);
			}

			await SaveEncounter(encounter, cancellationToken);
		}

		public async Task RollInitiativeAsync(Guid encounterId, Guid userId, bool npcOnly, CancellationToken cancellationToken)
		{
			var encounter = await GetEncounterAsync(encounterId, cancellationToken);

			foreach (var participant in encounter.Participants)
			{
				if (participant.Type != EncounterParticipantType.Character || !npcOnly)
				{
					await strategies.RollParticipantInitiative[participant.Type].RollInitiative(participant, cancellationToken);
				}
			}

			if (encounter.InInitiativePhase)
			{
				encounter.OrderByInitiative();
			}

			await SaveEncounter(encounter, cancellationToken);
		}

		public async Task SetInitiativeForParticipantAsync(Guid encounterId, Guid userId, Guid participantId, int initiative, CancellationToken cancellationToken)
		{
			var encounter = await GetEncounterAsync(encounterId, cancellationToken);
			var participant = encounter.Participants.First(x => x.Id == participantId)
				?? throw new BusinessLogicException("Participant not found.");

			participant.Initiative = initiative;

			if (encounter.InInitiativePhase)
			{
				encounter.OrderByInitiative();
			}

			await SaveEncounter(encounter, cancellationToken);
		}

		public async Task RollInitiativeForParticipantAsync(Guid encounterId, Guid userId, Guid participantId, string skillName, CancellationToken cancellationToken)
		{
			var encounter = await GetEncounterAsync(encounterId, cancellationToken);
			var participant = encounter.Participants.First(x => x.Id == participantId)
				?? throw new BusinessLogicException("Participant not found.");

			await strategies.RollParticipantInitiative[participant.Type].RollInitiative(participant, cancellationToken, skillName);

			if (encounter.InInitiativePhase)
			{
				encounter.OrderByInitiative();
			}

			await SaveEncounter(encounter, cancellationToken);
		}

		public async Task ClearInitiativeAsync(Guid encounterId, CancellationToken cancellationToken)
		{
			var encounter = await GetEncounterAsync(encounterId, cancellationToken);

			foreach (var participant in encounter.Participants)
			{
				participant.Initiative = null;
			}

			if (encounter.InInitiativePhase)
			{
				encounter.OrderByInitiative();
			}

			await SaveEncounter(encounter, cancellationToken);
		}

		public async Task UpdateTurnAsync(Guid encounterId, bool moveForward, CancellationToken cancellationToken)
		{
			var encounter = await GetEncounterAsync(encounterId, cancellationToken);

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

		public async Task AddConditionToParticipantAsync(Guid encounterId, Guid participantId, string conditionName, CancellationToken cancellationToken)
		{
			var encounter = await GetEncounterAsync(encounterId, cancellationToken);
			var participant = encounter.Participants.First(x => x.Id == participantId)
				?? throw new BusinessLogicException("Participant not found.");

			await strategies.Conditions[participant.Type].AddConditionToParticipant(participant, conditionName, cancellationToken);
			await SaveEncounter(encounter, cancellationToken);
		}

		public Task EditConditionOnParticipantAsync(Guid encounterId, Guid participantId, string conditionName, int? level, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task DeleteConditionFormParticipantAsync(Guid encounterId, Guid participantId, string conditionName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Private functions

		private async Task SaveEncounter(Encounter encounter, CancellationToken cancellationToken)
		{
			encounterRepository.Save(encounter);
			await encounterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new EncounterUpdatedNotification(encounter), cancellationToken);
		}

		#endregion
	}
}
