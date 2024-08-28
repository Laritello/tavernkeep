using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Values
{
	public class SkillIncreaseValue : BuildValue
	{
		public List<SkillType> Selected { get; set; } = [];
	}
}
