using NCalc;
using Tavernkeep.Domain.Contracts.Structures;

namespace Tavernkeep.Domain.Entities.Pathfinder.Conditions
{
	public class CharacterConditionRecord : ConditionRecord
	{
		public required Character Character { get; set; }

		#region Methods

		protected override ICollection<int> Calculate(IEnumerable<Modifier> modifiers)
		{
			return modifiers.Select(x =>
			{
				var expression = new Expression(x.Formula);
				expression.Parameters["target_level"] = Character.Level;
				expression.Parameters["condition_level"] = Level;

				return Convert.ToInt32(expression.Evaluate());
			}).ToList();
		}

		#endregion
	}
}
