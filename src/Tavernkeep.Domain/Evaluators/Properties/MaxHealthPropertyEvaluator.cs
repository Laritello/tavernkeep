using Tavernkeep.Domain.Contracts.Interfaces;
using Tavernkeep.Domain.Entities.Pathfinder;
using Tavernkeep.Domain.Entities.Pathfinder.Properties;
using Tavernkeep.Domain.Evaluators.Modifiers;

namespace Tavernkeep.Domain.Evaluators.Properties
{
	public class MaxHealthPropertyEvaluator(Health health) : IValueEvaluator<int>
	{
		private readonly Character _character = health.Owner;
		private readonly CharacterModifierEvaluator _modifierEvaluator = new(health.Owner, "MaxHealth");

		public int Value => Calculate();

		private int Calculate()
		{
			return _character.Ancestry.Health + _character.Class.HealthPerLevel * _character.Level +
				_character.Abilities["Constitution"].Modifier * _character.Level + _modifierEvaluator.Value;
		}
	}
}
