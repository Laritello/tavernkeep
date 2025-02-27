using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Evaluators.Modifiers;

namespace Tavernkeep.Core.Evaluators.Properties
{
	public class SpeedPropertyEvaluator(Speed speed) : IValueEvaluator<int>
	{
		private readonly Speed _speed = speed;
		private readonly CharacterModifierEvaluator _modifierEvaluator = new(speed.Owner, "Speed");
		public int Value => Calculate();

		private int Calculate()
		{
			return _speed.Base + _modifierEvaluator.Value;
		}
	}
}
