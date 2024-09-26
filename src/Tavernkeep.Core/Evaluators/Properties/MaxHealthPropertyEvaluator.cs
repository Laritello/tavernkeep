using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Core.Evaluators.Properties
{
	public class MaxHealthPropertyEvaluator(Health health) : IValueEvaluator<int>
	{
		private readonly Character _character = health.Owner;
		public int Value => Calculate();

		private int Calculate()
		{
			var ancestryHealth = _character.Build.Ancestry.HitPoints;
			var classHealth = _character.Build.Class.HitPoints;
			var constitution = _character.Constitution.Modifier;

			return ancestryHealth + (classHealth + constitution) * _character.Level;
		}
	}
}
