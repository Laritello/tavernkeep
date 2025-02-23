using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Core.Entities.Encounters.Participants
{
	public class CharacterEncounterParticipant : EncounterParticipant
	{
		public Guid CharacterId { get; set; }
		public required Character Character { get; set; }
		public override EncounterParticipantType Type => EncounterParticipantType.Character;

		[NotMapped]
		public override IReadOnlyCollection<ConditionRecord> Conditions => Character.Conditions;
	}
}
