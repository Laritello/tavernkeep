using System.Text.Json.Serialization;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Values
{
	[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]

	[JsonDerivedType(typeof(AbilityModifierValue), nameof(AbilityModifierValue))]
	[JsonDerivedType(typeof(AbilityBoostValue), nameof(AbilityBoostValue))]
	[JsonDerivedType(typeof(AbilityFlawValue), nameof(AbilityFlawValue))]
	[JsonDerivedType(typeof(SkillIncreaseValue), nameof(SkillIncreaseValue))]
	public abstract class BuildValue
	{
		public Guid Id { get; set; }
	}
}
