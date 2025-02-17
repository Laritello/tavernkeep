using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Entities.Encounters.Participants
{
	public class CreatureEncounterParticipant : EncounterParticipant
	{
		public required Creature Creature { get; set; }
		public override EncounterParticipantType Type => EncounterParticipantType.Character;
	}
}
