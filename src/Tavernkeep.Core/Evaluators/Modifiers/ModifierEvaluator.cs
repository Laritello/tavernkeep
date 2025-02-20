using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Evaluators.Modifiers
{
	public class ModifierEvaluator(Character character, string target) : IValueEvaluator<int>
	{
		private readonly TypeModifierEvaluator _circumstanceModifierEvaluator = new(character, target, ModifierType.Circumstance);
		private readonly TypeModifierEvaluator _statusModifierEvaluator = new(character, target, ModifierType.Status);
		private readonly TypeModifierEvaluator _itemModifierEvaluator = new(character, target, ModifierType.Item);

		public int Value => _statusModifierEvaluator.Value + _circumstanceModifierEvaluator.Value + _itemModifierEvaluator.Value;

		private class TypeModifierEvaluator(Character character, string target, ModifierType type) : IValueEvaluator<int>
		{
			private readonly Character _character = character;
			private readonly string _target = target;
			private readonly ModifierType _type = type;

			public int Value => Calculate();

			private int Calculate()
			{
				var modifiers = _character.Conditions
					.Where(cr => cr.HasModifier(_target, _type))
					.Select(cr => cr[_target])
					.ToList();

				var penalty = modifiers.Count > 0 && modifiers.Min() < 0 ? modifiers.Min() : 0;
				var bonus = modifiers.Count > 0 && modifiers.Max() > 0 ? modifiers.Max() : 0;

				return bonus + penalty;
			}
		}
	}
}
