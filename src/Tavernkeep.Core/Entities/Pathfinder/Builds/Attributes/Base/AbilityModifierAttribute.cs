using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base
{
	public abstract class AbilityModifierAttribute : BuildAttribute
	{
		public List<AbilityType> Selected { get; set; } = [];
		public bool Contains(AbilityType type) => Selected != null && Selected.Contains(type);
	}
}
