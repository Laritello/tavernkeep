using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Evaluators.Modifiers
{
	public class ModifierEvaluator(Character character, ModifierTarget target) : IValueEvaluator<int>
	{
		private readonly TypeModifierEvaluator _circumstanceModifierEvaluator = new(character, target, ModifierType.Circumstance);
		private readonly TypeModifierEvaluator _statusModifierEvaluator = new(character, target, ModifierType.Status);
		private readonly TypeModifierEvaluator _itemModifierEvaluator = new(character, target, ModifierType.Item);

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

				var totalBonus = (activeBonus != null ? activeBonus.Value : 0) - (activePenalty != null ? activePenalty.Value : 0);

				return totalBonus;
			}
		}
	}
}
