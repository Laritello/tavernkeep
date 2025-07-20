using Tavernkeep.Domain.Contracts.Interfaces;
using Tavernkeep.Domain.Entities.Pathfinder.Properties;
using Tavernkeep.Domain.Evaluators.Modifiers;

namespace Tavernkeep.Domain.Evaluators.Properties
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
