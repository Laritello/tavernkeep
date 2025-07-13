using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Core.Evaluators.Modifiers
{
	public class CreatureModifierEvaluator(IEnumerable<CreatureConditionRecord> conditions, string target) : IValueEvaluator<int>
	{
		private static readonly Dictionary<string, List<string>> targets = new()
		{
			{ "Acrobatics", ["Acrobatics", "Dexterity", "SkillChecks", "AllChecks"] },
			{ "Arcana", ["Arcana", "Intelligence", "SkillChecks", "AllChecks"] },
			{ "Athletics", ["Athletics", "Strength", "SkillChecks", "AllChecks"] },
			{ "Crafting", ["Crafting", "Intelligence", "SkillChecks", "AllChecks"] },
			{ "Deception", ["Deception", "Charisma", "SkillChecks", "AllChecks"] },
			{ "Diplomacy", ["Diplomacy", "Charisma", "SkillChecks", "AllChecks"] },
			{ "Intimidation", ["Intimidation", "Charisma", "SkillChecks", "AllChecks"] },
			{ "Medicine", ["Medicine", "Wisdom", "SkillChecks", "AllChecks"] },
			{ "Nature", ["Nature", "Wisdom", "SkillChecks", "AllChecks"] },
			{ "Occultism", ["Occultism", "Intelligence", "SkillChecks", "AllChecks"] },
			{ "Performance", ["Performance", "Charisma", "SkillChecks", "AllChecks"] },
			{ "Religion", ["Religion", "Wisdom", "SkillChecks", "AllChecks"] },
			{ "Society", ["Society", "Intelligence", "SkillChecks", "AllChecks"] },
			{ "Stealth", ["Stealth", "Dexterity", "SkillChecks", "AllChecks"] },
			{ "Survival", ["Survival", "Wisdom", "SkillChecks", "AllChecks"] },
			{ "Thievery", ["Thievery", "Dexterity", "SkillChecks", "AllChecks"] },
			{ "Lore", ["Intelligence", "SkillChecks", "AllChecks"] },
			{ "Fortitude", ["Fortitude", "Constitution", "SkillChecks", "AllChecks"] },
			{ "Reflex", ["Reflex", "Dexterity", "SkillChecks", "AllChecks"] },
			{ "Will", ["Will", "Wisdom", "SkillChecks", "AllChecks"] },
			{ "Ranged", ["Dexterity", "SkillChecks", "AllChecks"] },
			{ "Melee", ["Strength", "SkillChecks", "AllChecks"] }
		};

		private readonly TypeModifierEvaluator _circumstanceModifierEvaluator = new(conditions, ModifierType.Circumstance, targets[target]);
		private readonly TypeModifierEvaluator _statusModifierEvaluator = new(conditions, ModifierType.Status, targets[target]);
		private readonly TypeModifierEvaluator _itemModifierEvaluator = new(conditions, ModifierType.Item, targets[target]);

		public int Value => _statusModifierEvaluator.Value + _circumstanceModifierEvaluator.Value + _itemModifierEvaluator.Value;

		private class TypeModifierEvaluator(IEnumerable<CreatureConditionRecord> conditions, ModifierType type, params ICollection<string> target) : IValueEvaluator<int>
		{
			private readonly IEnumerable<CreatureConditionRecord> _conditions = conditions;
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
