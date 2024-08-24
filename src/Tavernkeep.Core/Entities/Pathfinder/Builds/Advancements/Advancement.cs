using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements
{
	[JsonDerivedType(typeof(AbilityBoostAdvancement), typeDiscriminator: nameof(AbilityBoostAdvancement))]
	[JsonDerivedType(typeof(AbilityFlawAdvancement), typeDiscriminator: nameof(AbilityFlawAdvancement))]
	[JsonDerivedType(typeof(KeyAbilityAdvancement), typeDiscriminator: nameof(KeyAbilityAdvancement))]
	[JsonDerivedType(typeof(SkillIncreaseAdvancement), typeDiscriminator: nameof(SkillIncreaseAdvancement))]
	[JsonDerivedType(typeof(IntelligenceBasedSkillIncreaseAdvancement), typeDiscriminator: nameof(IntelligenceBasedSkillIncreaseAdvancement))]
	[JsonDerivedType(typeof(PerceptionProficiencyAdvancement), typeDiscriminator: nameof(PerceptionProficiencyAdvancement))]
	public class Advancement
	{
		public int Level { get; set; } 
	}
}
