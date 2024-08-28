using System.Text.Json.Serialization;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.AbilityBoost;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.AbilityFlaw;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.SkillIncrease;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Snapshots;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Values;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base
{
	[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]

	[JsonDerivedType(typeof(AbilityModifierAttribute), nameof(AbilityModifierAttribute))]

	[JsonDerivedType(typeof(SkillIncreaseAttribute), nameof(SkillIncreaseAttribute))]
	[JsonDerivedType(typeof(AbilityBoostAttribute), nameof(AbilityBoostAttribute))]
	[JsonDerivedType(typeof(AbilityFlawAttribute), nameof(AbilityFlawAttribute))]

	[JsonDerivedType(typeof(FixedAbilityBoostAttribute), nameof(FixedAbilityBoostAttribute))]
	[JsonDerivedType(typeof(FixedKeyAbilityAttribute), nameof(FixedKeyAbilityAttribute))]
	[JsonDerivedType(typeof(VariableAbilityBoostAttribute), nameof(VariableAbilityBoostAttribute))]
	[JsonDerivedType(typeof(VariableKeyAbilityAttribute), nameof(VariableKeyAbilityAttribute))]

	[JsonDerivedType(typeof(FixedAbilityFlawAttribute), nameof(FixedAbilityFlawAttribute))]

	[JsonDerivedType(typeof(FixedSkillIncreaseAttribute), nameof(FixedSkillIncreaseAttribute))]
	[JsonDerivedType(typeof(VariableSkillIncreaseAttribute), nameof(VariableSkillIncreaseAttribute))]
	[JsonDerivedType(typeof(BonusSkillIncreaseAttribute), nameof(BonusSkillIncreaseAttribute))]
	public abstract class BuildAttribute
	{
		public Guid Id { get; set; }
		public int Level { get; set; }
		public abstract BuildAttribute Convert(BuildSnapshot snapshot, ConversionParameters? parameters = null);
		public abstract BuildAttribute Convert(List<BuildValue> values, ConversionParameters? parameters = null);
	}
}
