using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Values
{
	public class AbilityBoostValue : AbilityModifierValue
	{
		public List<AbilityType> Selected { get; set; } = [];
	}
}
