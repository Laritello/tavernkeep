using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Library.Creatures;
using Tavernkeep.Domain.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Domain.Entities.Encounters.Participants;

public class CreatureEncounterParticipant : EncounterParticipant
{
	#region Backing fields

	private readonly List<CreatureConditionRecord> _conditions = [];

	#endregion

	public Guid OriginId { get; set; }
	public required Creature Origin { get; set; }
	public int CurrentHealth { get; set; }
	public int TemporaryHealth { get; set; }
	public override EncounterParticipantType Type => EncounterParticipantType.Creature;
	public IReadOnlyCollection<CreatureConditionRecord> Conditions => _conditions.AsReadOnly();

	[NotMapped]
	public Creature Creature { get; set; }

	public void AddCondition(CreatureConditionRecord conditionRecord)
	{
		_conditions.Add(conditionRecord);
	}

	public void RemoveCondition(CreatureConditionRecord conditionRecord)
	{
		_conditions.Remove(conditionRecord);
	}
}
