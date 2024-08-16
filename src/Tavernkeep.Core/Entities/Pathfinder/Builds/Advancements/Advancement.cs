using System.Text.Json.Serialization;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements
{
	[JsonDerivedType(typeof(AbilityBoostAdvancement), typeDiscriminator: nameof(AbilityBoostAdvancement))]
	[JsonDerivedType(typeof(AbilityFlawAdvancement), typeDiscriminator: nameof(AbilityFlawAdvancement))]
	public class Advancement { }
}
