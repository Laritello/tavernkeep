using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Core.Evaluators.Modifiers
{
	public class CreatureModifierEvaluator(ICollection<CreatureConditionRecord> conditions, params ICollection<string> target) : IValueEvaluator<int>
	{
		private readonly TypeModifierEvaluator _circumstanceModifierEvaluator = new(conditions, ModifierType.Circumstance, target);
		private readonly TypeModifierEvaluator _statusModifierEvaluator = new(conditions, ModifierType.Status, target);
		private readonly TypeModifierEvaluator _itemModifierEvaluator = new(conditions, ModifierType.Item, target);

		public int Value => _statusModifierEvaluator.Value + _circumstanceModifierEvaluator.Value + _itemModifierEvaluator.Value;

		private class TypeModifierEvaluator(ICollection<CreatureConditionRecord> conditions, ModifierType type, params ICollection<string> target) : IValueEvaluator<int>
		{
			private readonly ICollection<CreatureConditionRecord> _conditions = conditions;
			private readonly ICollection<string> _target = target;
			private readonly ModifierType _type = type;

			public int Value => Calculate();

			private int Calculate()
			{
				var modifiers = _conditions.SelectMany(x => x.Collect(_target, _type)).ToList();

				var penalty = modifiers.Count > 0 && modifiers.Min() < 0 ? modifiers.Min() : 0;
				var bonus = modifiers.Count > 0 && modifiers.Max() > 0 ? modifiers.Max() : 0;

				return bonus + penalty;
			}
		}
	}
}
