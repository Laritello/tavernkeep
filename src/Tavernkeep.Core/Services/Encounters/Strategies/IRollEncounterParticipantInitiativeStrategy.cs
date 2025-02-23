using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters.Participants;

namespace Tavernkeep.Core.Services.Encounters.Strategies
{
	public interface IRollEncounterParticipantInitiativeStrategy
	{
		public EncounterParticipantType Type { get; }
		public Task RollInitiative(EncounterParticipant participant, CancellationToken cancellationToken, string skillName = "Perception");
	}
}
