using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters.Participants;

namespace Tavernkeep.Core.Strategies.Encounters
{
	public interface IEncounterParticipantConditionStrategy
	{
		public EncounterParticipantType Type { get; }
		Task AddConditionToParticipant(EncounterParticipant participant, string conditionName, CancellationToken cancellationToken);
	}
}
