using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements
{
	public class PerceptionProficiencyAdvancement : Advancement
	{
		public Proficiency? Proficiency { get; set; }

		public override string ToString() => $"PerceptionProficiencyAdvancement [Level {Level}]: {(Proficiency.HasValue ? Proficiency : "Not Selected")}";
	}
}
