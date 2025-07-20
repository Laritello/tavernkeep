using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Encounters.Participants;

namespace Tavernkeep.Domain.Strategies.Encounters
{
	public interface IRollEncounterParticipantInitiativeStrategy
	{
		public EncounterParticipantType Type { get; }
		public Task RollInitiative(EncounterParticipant participant, CancellationToken cancellationToken, string skillName = "Perception");
	}
}
