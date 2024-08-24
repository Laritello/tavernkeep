using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements
{
	public class SkillIncreaseAdvancement : Advancement
	{
		public bool IsFree { get; set; }
		public List<SkillType> Possible { get; set; } = [];
		public SkillType? Selected { get; set; }
		public Proficiency MaximumAllowedProficiency { get; set; } = Proficiency.Trained;
		public override string ToString() => $"SkillIncreaseAdvancement [Level {Level}]: {(Selected.HasValue ? Selected : "Not Selected")} / {MaximumAllowedProficiency}";
	}
}
