using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Encounters.Participants;

namespace Tavernkeep.Domain.Strategies.Encounters;

public interface IEncounterParticipantConditionStrategy
{
	public EncounterParticipantType Type { get; }
	Task AddConditionToParticipant(EncounterParticipant participant, string conditionName, CancellationToken cancellationToken);
	Task EditConditionOnParticipant(EncounterParticipant participant, string conditionName, int level, CancellationToken cancellationToken);
	Task DeleteConditionFromParticipant(EncounterParticipant participant, string conditionName, CancellationToken cancellationToken);
}
