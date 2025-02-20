using NCalc;

namespace Tavernkeep.Core.Entities.Pathfinder.Conditions
{
	public class CreatureConditionRecord : ConditionRecord
	{
		public required Creature Creature { get; set; }
		public override int this[string targetName]
		{
			get
			{
				var expression = new Expression(Condition.Modifiers[targetName].Formula);
				expression.Parameters["target_level"] = Creature.Level;
				expression.Parameters["condition_level"] = Level;

				return Convert.ToInt32(expression.Evaluate());
			}
		}
	}
}
