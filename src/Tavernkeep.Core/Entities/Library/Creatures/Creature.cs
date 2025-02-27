using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Evaluators.Modifiers;

namespace Tavernkeep.Core.Entities.Library.Creatures
{
	[Table("Creature")]
	public class Creature : GuidEntity
	{
		public required string Name { get; set; }
		public int Level { get; set; }
		public UnitSize Size { get; set; }
		public Rarity Rarity { get; set; }
		public int Perception { get; set; }
		public int ArmorClass { get; set; }
		public required HealthInformation Health { get; set; }
		public required Dictionary<string, int> Abilities { get; set; }
		public required Dictionary<string, int> Skills { get; set; }
		public required Dictionary<string, int> SavingThrows { get; set; }
		public required List<Sense> Senses { get; set; }
		public required List<string> Languages { get; set; }
		public required List<string> Traits { get; set; }
		public required List<Resistance> Resistances { get; set; }
		public required List<Weakness> Weaknesses { get; set; }
		public required List<SpeedInformation> Speeds { get; set; }
		public required Dictionary<string, string> Notes { get; set; }

		public Creature Copy()
		{
			return new Creature
			{
				Name = Name,
				Level = Level,
				Size = Size,
				Rarity = Rarity,
				Perception = Perception,
				ArmorClass = ArmorClass,
				Health = new() { Max = Health.Max, Temporary = Health.Temporary },
				Abilities = Abilities.ToDictionary(x => x.Key, x => x.Value),
				Skills = Skills.ToDictionary(x => x.Key, x => x.Value),
				SavingThrows = SavingThrows.ToDictionary(x => x.Key, x => x.Value),
				Senses = Senses,
				Languages = Languages, 
				Traits = Traits,
				Resistances = Resistances,
				Weaknesses = Weaknesses,
				Speeds = Speeds.Select(x => new SpeedInformation { Type = x.Type, Value = x.Value }).ToList(),
				Notes = Notes,
			};
		}

		public Creature ApplyConditions(ICollection<CreatureConditionRecord> conditions)
		{
			var perception = new CreatureModifierEvaluator(conditions, "Perception", "SkillChecks", "AllChecks");
			var armorClass = new CreatureModifierEvaluator(conditions, "ArmorClass", "Dexterity", "AllChecks");
			var health = new CreatureModifierEvaluator(conditions, "MaxHealth");

			Perception += perception.Value;
			ArmorClass += armorClass.Value;
			Health.Max += health.Value;

			foreach (var skill in Skills)
			{
				var evaluator = new CreatureModifierEvaluator(conditions, _typeToTargets[skill.Key]);
				Skills[skill.Key] += evaluator.Value;
			}

			foreach (var savingThrow in SavingThrows)
			{
				var evaluator = new CreatureModifierEvaluator(conditions, _typeToTargets[savingThrow.Key]);
				SavingThrows[savingThrow.Key] += evaluator.Value;
			}

			foreach (var speed in Speeds)
			{
				var evaluator = new CreatureModifierEvaluator(conditions, "Speed");
				speed.Value += evaluator.Value;
			}

			return this;
		}

		private static readonly Dictionary<string, string[]> _typeToTargets = new()
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

			{ "Will", ["Will", "Wisdom", "SkillChecks", "AllChecks"] },
			{ "Reflex", ["Reflex", "Dexterity", "SkillChecks", "AllChecks"] },
			{ "Fortitude", ["Fortitude", "Constitution", "SkillChecks", "AllChecks"] },
		};
	}
}
