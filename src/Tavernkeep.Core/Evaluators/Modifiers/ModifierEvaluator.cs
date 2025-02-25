using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Evaluators.Modifiers
{
	public class ModifierEvaluator(Character character, params ICollection<string> target) : IValueEvaluator<int>
	{
		private readonly TypeModifierEvaluator _circumstanceModifierEvaluator = new(character, ModifierType.Circumstance, target);
		private readonly TypeModifierEvaluator _statusModifierEvaluator = new(character, ModifierType.Status, target);
		private readonly TypeModifierEvaluator _itemModifierEvaluator = new(character, ModifierType.Item, target);

		public int Value => _statusModifierEvaluator.Value + _circumstanceModifierEvaluator.Value + _itemModifierEvaluator.Value;

		private class TypeModifierEvaluator(Character character, ModifierType type, params ICollection<string> target) : IValueEvaluator<int>
		{
			private readonly Character _character = character;
			private readonly ICollection<string> _target = target;
			private readonly ModifierType _type = type;

			public int Value => Calculate();

			private int Calculate()
			{
				var modifiers = _character.Conditions.SelectMany(x => x.Collect(_target, _type)).ToList();

				var penalty = modifiers.Count > 0 && modifiers.Min() < 0 ? modifiers.Min() : 0;
				var bonus = modifiers.Count > 0 && modifiers.Max() > 0 ? modifiers.Max() : 0;

				return bonus + penalty;
			}
		}
	}
}
