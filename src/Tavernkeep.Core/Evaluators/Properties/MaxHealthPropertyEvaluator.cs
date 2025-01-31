using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Evaluators.Modifiers;

namespace Tavernkeep.Core.Evaluators.Properties
{
	public class MaxHealthPropertyEvaluator(Health health) : IValueEvaluator<int>
	{
		private readonly Character _character = health.Owner;
		private readonly ModifierEvaluator _modifierEvaluator = new(health.Owner, "MaxHealth");

		public int Value => Calculate();

		private int Calculate()
		{
			return _character.Ancestry.Health + _character.Class.HealthPerLevel * _character.Level +
				_character.Abilities["Constitution"].Modifier * _character.Level + _modifierEvaluator.Value;
		}
	}
}
