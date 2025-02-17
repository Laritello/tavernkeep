using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Entities.Encounters.Participants
{
	public class CreatureEncounterParticipant : EncounterParticipant
	{
		public Guid CreatureId { get; set; }
		public required Creature Creature { get; set; }
		public override EncounterParticipantType Type => EncounterParticipantType.Creature;
	}
}
