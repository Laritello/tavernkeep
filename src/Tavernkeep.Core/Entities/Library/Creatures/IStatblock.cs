using System.Text.Json.Serialization;
using Tavernkeep.Core.Entities.Library.Creatures.Statblocks;

namespace Tavernkeep.Core.Entities.Library.Creatures
{
	[JsonDerivedType(typeof(AbilityStatblock), nameof(AbilityStatblock))]
	[JsonDerivedType(typeof(AttackStatblock), nameof(AttackStatblock))]
	[JsonDerivedType(typeof(AttributesStatblock), nameof(AttributesStatblock))]
	[JsonDerivedType(typeof(DefenseStatblock), nameof(DefenseStatblock))]
	[JsonDerivedType(typeof(GeneralStatblock), nameof(GeneralStatblock))]
	[JsonDerivedType(typeof(HealthStatblock), nameof(HealthStatblock))]
	[JsonDerivedType(typeof(LanguageStatblock), nameof(LanguageStatblock))]
	[JsonDerivedType(typeof(PerceptionStatblock), nameof(PerceptionStatblock))]
	[JsonDerivedType(typeof(SkillsStatblock), nameof(SkillsStatblock))]
	[JsonDerivedType(typeof(SpeedStatblock), nameof(SpeedStatblock))]
	[JsonDerivedType(typeof(SpellsStatblock), nameof(SpellsStatblock))]
	[JsonDerivedType(typeof(TraitsStatblock), nameof(TraitsStatblock))]
	public interface IStatblock
	{
		public bool IsDividerEnabled { get; set; }
		public string HTML { get; }
	}
}
