using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Contracts.Structures;
using Tavernkeep.Domain.Entities.Base;
using Tavernkeep.Domain.Entities.Library.Conditions;

namespace Tavernkeep.Domain.Entities.Pathfinder.Conditions;

[Table("ConditionRecord")]
public abstract class ConditionRecord : GuidEntity
{
	#region Constructors

	public ConditionRecord() { }

	#endregion

	#region Properties

	public required Condition Condition { get; set; }
	public int? Level { get; set; }

	#endregion

	#region Methods

	public ICollection<int> Collect(ICollection<string> targets, ModifierType type)
	{
		var modifiers = Condition.Modifiers
			.Where(x => targets.Contains(x.Key) && x.Value.Type == type)
			.Select(x => x.Value);
		var related = Condition.Related.SelectMany(x => x.Collect(targets, type));

		return [.. Calculate(modifiers), .. related];
	}

	protected abstract ICollection<int> Calculate(IEnumerable<Modifier> modifiers);

	#endregion
}
