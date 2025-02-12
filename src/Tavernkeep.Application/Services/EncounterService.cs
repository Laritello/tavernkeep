using Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterCreated;
using Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterUpdated;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters;
using Tavernkeep.Core.Entities.Encounters.Participants;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Services;
using Tavernkeep.Core.Specifications.Encounters;

namespace Tavernkeep.Application.Services
{
	public class EncounterService(
		IEncounterRepository encounterRepository,
		ICharacterRepository characterRepository,
		INotificationService notificationService
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
			return await encounterRepository.GetFullEncounterAsync(encounterId, cancellationToken) ??
				throw new BusinessLogicException("Encounter not found");
		}

		public async Task UpdateEncounterStatusAsync(Guid encounterId, EncounterStatus status, CancellationToken cancellationToken)
		{
			var encounter = await encounterRepository.FindAsync(encounterId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Encounter not found.");

			encounter.Status = status;

			encounterRepository.Save(encounter);
			await encounterRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new EncounterUpdatedNotification(encounter), cancellationToken);
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
			var character = await characterRepository.GetFullCharacterAsync(characterId, cancellationToken)
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
	}
}
