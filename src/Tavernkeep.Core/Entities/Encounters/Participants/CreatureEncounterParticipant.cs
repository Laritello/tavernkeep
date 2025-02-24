using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Core.Entities.Encounters.Participants
{
	public class CreatureEncounterParticipant : EncounterParticipant
	{
		#region Backing fields

		private readonly List<CreatureConditionRecord> _conditions = [];

		#endregion

		public Guid CreatureId { get; set; }
		public int CurrentHealth { get; set; }
		public int TemporaryHealth { get; set; }
		public required Creature Creature { get; set; }
		public override EncounterParticipantType Type => EncounterParticipantType.Creature;
		public IReadOnlyCollection<CreatureConditionRecord> Conditions => _conditions.AsReadOnly();

		public void AddCondition(CreatureConditionRecord conditionRecord)
		{
			_conditions.Add(conditionRecord);
		}

		public void RemoveCondition(CreatureConditionRecord conditionRecord)
		{
			_conditions.Remove(conditionRecord);
		}
	}
}
