using System.Text.Json.Serialization;
using Tavernkeep.Core.Statblocks.Components;

namespace Tavernkeep.Core.Statblocks.Abstractions
{
	[JsonDerivedType(typeof(AbilityStatblockComponent), nameof(AbilityStatblockComponent))]
	[JsonDerivedType(typeof(AttackStatblockComponent), nameof(AttackStatblockComponent))]
	[JsonDerivedType(typeof(AttributesStatblockComponent), nameof(AttributesStatblockComponent))]
	[JsonDerivedType(typeof(DefenseStatblockComponent), nameof(DefenseStatblockComponent))]
	[JsonDerivedType(typeof(GeneralStatblockComponent), nameof(GeneralStatblockComponent))]
	[JsonDerivedType(typeof(HealthStatblockComponent), nameof(HealthStatblockComponent))]
	[JsonDerivedType(typeof(LanguagesStatblockComponent), nameof(LanguagesStatblockComponent))]
	[JsonDerivedType(typeof(PerceptionStatblockComponent), nameof(PerceptionStatblockComponent))]
	[JsonDerivedType(typeof(SkillsStatblockComponent), nameof(SkillsStatblockComponent))]
	[JsonDerivedType(typeof(SpeedStatblockComponent), nameof(SpeedStatblockComponent))]
	[JsonDerivedType(typeof(SpellsStatblockComponent), nameof(SpellsStatblockComponent))]
	[JsonDerivedType(typeof(TraitsStatblockComponent), nameof(TraitsStatblockComponent))]
	[JsonDerivedType(typeof(RitualsStatblockComponent), nameof(RitualsStatblockComponent))]
	public interface IStatblockComponent
	{
		public bool IsDividerEnabled { get; set; }

		[JsonIgnore]
		public string HTML { get; }
	}
}
