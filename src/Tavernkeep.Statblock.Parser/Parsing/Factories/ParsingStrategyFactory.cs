using Tavernkeep.Core.Statblocks;
using Tavernkeep.Statblock.Parser.Parsing.Abstractions;
using Tavernkeep.Statblock.Parser.Parsing.Factories.Components;

namespace Tavernkeep.Statblock.Parser.Parsing.Factories
{
	public static class ParsingStrategyFactory
	{
		public static IStatblockComponentFactory GetFactory(string name)
		{
			return name switch
			{
				"Perception" => new PerceptionStatblockComponentFactory(),
				"Languages" => new LanguagesStatblockComponentFactory(),
				"Skills" => new SkillsStatblockComponentFactory(),
				"Str" or "Dex" or "Int" or "Con" or "Wis" or "Cha" => new AttributesStatblockComponentFactory(),
				"AC" or "Fort" or "Ref" or "Will" => new DefenseStatblockComponentFactory(),
				"HP" or "Weaknesses" or "Immunities" or "Resistances" => new HealthStatblockComponentFactory(),
				"Melee" or "Ranged" => new AttackStatblockComponentFactory(),
				"Speed" => new SpeedStatblockComponentFactory(),
				"Rituals" or "Ritual" => new RitualsStatblockComponentFactory(),
				string a when StatblockRegexes.SpellsHeader().IsMatch(a) => new SpellsStatblockComponentFactory(),
				_ => new AbilityStatblockComponentFactory(),
			};
		}
	}
}
