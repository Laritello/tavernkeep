using NCalc;

namespace Tavernkeep.Core.Entities.Pathfinder.Conditions
{
	public class CreatureConditionRecord : ConditionRecord
	{
		public required Creature Creature { get; set; }
		public override int this[ICollection<string> targetNames]
		{
			get
			{
				foreach (var name in targetNames)
				{
					if (Condition.Modifiers.TryGetValue(name, out var modifier))
					{
						var expression = new Expression(modifier.Formula);
						expression.Parameters["target_level"] = Creature.Level;
						expression.Parameters["condition_level"] = Level;

						return Convert.ToInt32(expression.Evaluate());
					}
				}

				return 0;
			}
		}
	}
}
