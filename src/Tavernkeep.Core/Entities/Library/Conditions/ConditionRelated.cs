using NCalc;
using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities.Library.Conditions
{
	[Table("ConditionRelated")]
	public class ConditionRelated : GuidEntity
	{
		public required Condition Owner { get; set; }
		public required Condition Condition { get; set; }
		public int? Level { get; set; }

		public ICollection<int> Collect(ICollection<string> targets, ModifierType type)
		{
			return [.. Condition.Modifiers
				.Where(x => targets.Contains(x.Key) && x.Value.Type == type)
				.Select(x =>
				{
					var expression = new Expression(x.Value.Formula);
					expression.Parameters["condition_level"] = Level;

					return Convert.ToInt32(expression.Evaluate());
				})];
		}
	}
}
