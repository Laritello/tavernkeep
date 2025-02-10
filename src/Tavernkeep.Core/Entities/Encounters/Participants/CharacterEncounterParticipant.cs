using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Entities.Encounters.Participants
{
	public class CharacterEncounterParticipant : EncounterParticipant
	{
		public required Character Character { get; set; }
	}
}
