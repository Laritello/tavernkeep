using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Core.Evaluators.Properties
{
	public class SpeedPropertyEvaluator(Speed speed) : IValueEvaluator<int>
	{
		private readonly Speed _speed = speed;
		//private readonly ModifierEvaluator _modifierEvaluator = new(speed.Owner, speed.Type.ToTarget());
		public int Value => Calculate();

		private int Calculate()
		{
			//return _speed.Base + _modifierEvaluator.Value;
			return _speed.Base;
		}
	}
}
