using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base
{
	public abstract class SkillIncreaseAttribute : BuildAttribute
	{
		public List<SkillType> Selected { get; set; } = [];
		public Proficiency MaxProficiency { get; set; }

		public bool Contains(SkillType skillType) => Selected != null && Selected.Contains(skillType);
	}
}
