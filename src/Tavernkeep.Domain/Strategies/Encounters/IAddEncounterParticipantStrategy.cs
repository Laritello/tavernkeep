using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Encounters;

namespace Tavernkeep.Domain.Strategies.Encounters;

public interface IAddEncounterParticipantStrategy
{
	public EncounterParticipantType Type { get; }
	Task AddParticipantAsync(Encounter encounter, Guid entityId, CancellationToken cancellationToken);
}
