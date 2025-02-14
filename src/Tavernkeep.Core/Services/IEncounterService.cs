using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters;

namespace Tavernkeep.Core.Services
{
	public interface IEncounterService
	{
		public Task<ICollection<Encounter>> GetAllEncountersAsync(CancellationToken cancellationToken);
		public Task<Encounter> GetEncounterAsync(Guid encounterId, CancellationToken cancellationToken);
		public Task<Encounter> CreateEncounterAsync(string name, CancellationToken cancellationToken);
		public Task UpdateEncounterStatusAsync(Guid encounterId, EncounterStatus status, CancellationToken cancellationToken);
		public Task AddParticipantAsync(Guid encounterId, EncounterParticipantType type, Guid entityId, CancellationToken cancellationToken);
		public Task RemoveParticipantAsync(Guid encounterId, Guid participantId, CancellationToken cancellationToken);
		public Task UpdateParticipantsOrdinalAsync(Guid encounterId, IList<Guid> ordinals, CancellationToken cancellationToken);
		public Task RollInitiative(Guid encounterId, Guid userId, bool npcOnly, CancellationToken cancellationToken);
		public Task RollInitiativeForParticipant(Guid encounterId, Guid userId, Guid participantId, string skillName, CancellationToken cancellationToken);
	}
}
