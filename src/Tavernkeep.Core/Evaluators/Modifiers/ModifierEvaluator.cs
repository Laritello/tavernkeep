using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Evaluators.Modifiers
{
	public class ModifierEvaluator(Character character, ModifierTarget target) : IValueEvaluator<int>
	{
		private readonly IValueEvaluator<int> _circumstanceModifierEvaluator = new TypeModifierEvaluator(character, target, ModifierType.Circumstance);
		private readonly IValueEvaluator<int> _statusModifierEvaluator = new TypeModifierEvaluator(character, target, ModifierType.Status);
		private readonly IValueEvaluator<int> _itemModifierEvaluator = new TypeModifierEvaluator(character, target, ModifierType.Item);

		public int Value => _statusModifierEvaluator.Value + _circumstanceModifierEvaluator.Value + _itemModifierEvaluator.Value;

		private class TypeModifierEvaluator(Character character, ModifierTarget target, ModifierType type) : IValueEvaluator<int>
		{
			private readonly Character _character = character;
			private readonly ModifierTarget _target = target;
			private readonly ModifierType _type = type;

			public int Value => Calculate();

			private int Calculate()
			{
				var conditionModifiers = _character.Conditions
					.SelectMany(x => x.CollectModifiers(_character))
					.Where(x => x.Targets.Contains(_target))
					.ToList();

				var activeBonus = conditionModifiers.Where(x => x.Type == _type && x.IsBonus).MaxBy(x => x.Value);
				var activePenalty = conditionModifiers.Where(x => x.Type == _type && x.IsPenalty).MaxBy(x => x.Value);

				var totalBonus = (activeBonus != null ? activeBonus.Value : 0) + (activePenalty != null ? activePenalty.Value : 0);

				return totalBonus;
			}
		}
	}
}
