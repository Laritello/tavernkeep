using System.Text.RegularExpressions;

namespace Tavernkeep.Core.Statblocks
{
	public partial class StatblockRegexes
	{

		[GeneratedRegex("((?:Arcane|Divine|Elemental|Occult|Primal|Champion|Bard) (?:Innate|Spontaneous|Prepared|Devotion|Spells|Composition) (?:Spells|Known))")]
		public static partial Regex SpellsHeader();

		[GeneratedRegex("\\d+(?:th|rd|nd|st)")]
		public static partial Regex SpellsLevel();

		[GeneratedRegex("(?:Cantrips|Constant)")]
		public static partial Regex SpellsKeywords();

		[GeneratedRegex(@"(\[free-action\]|\[reaction\]|\[one-action\]|\[two-actions\]|\[three-actions\])")]
		public static partial Regex ActionsKeywords();

		[GeneratedRegex(@"^(Str|Dex|Int|Con|Wis|Cha)$")]
		public static partial Regex AttributesKeywords();

		[GeneratedRegex(@"^((?:\+|-)\d+)$")]
		public static partial Regex Modifier();

		[GeneratedRegex(@"^(?<modifier>(?:\+|-)\d+)(?<text>.+)")]
		public static partial Regex ModifierWithText();

		[GeneratedRegex(@"^(?:Trigger|Effect|Requirement|Requirements|Frequency|Critical|Success|Failure|Critical Success|Critical Failure|
			Saving|Throw|Saving Throw|Maximum|Duration|Maximum Duration|Stage \d+)$")]
		public static partial Regex AbilitySubKeywords();

		[GeneratedRegex(@"^(?:Fort|Ref|Will)$")]
		public static partial Regex DefenseSubKeywords();

		[GeneratedRegex(@"^(?:Damage|Effect)$")]
		public static partial Regex AttackSubKeywords();

		[GeneratedRegex(@"^(?:Weakness|Weaknesses|Immunity|Immunities|Resistance|Resistances|Hardness)$")]
		public static partial Regex HealthSubKeywords();

		[GeneratedRegex("(?<creatureName>.*)(?=CREATURE)(?<creatureType>CREATURE)(?<creatureLevel>.*)")]
		public static partial Regex GeneralInfromation();

		[GeneratedRegex(@"^(?<skillName>Acrobatics|Arcana|Athletics|Crafting|Deception|Diplomacy|Intimidation|Lore|Medicine|Nature|Occultism|Performance|Religion|Society|Stealth|Survival|Thievery)$")]
		public static partial Regex SkillName();
	}
}
