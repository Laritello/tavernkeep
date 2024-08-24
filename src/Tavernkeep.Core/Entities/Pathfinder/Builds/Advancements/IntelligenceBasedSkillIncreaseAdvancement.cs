using Tavernkeep.Core.Contracts.Interfaces;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements
{
	public class IntelligenceBasedSkillIncreaseAdvancement : Advancement, IAdvancementContainer
	{
		public int BaseAmount { get; set; }
		public ICollection<Advancement> Advancements { get; set; } = [];

		public override string ToString() => $"IntelligenceBasedSkillIncreaseAdvancement [Level {Level}]: {BaseAmount} + Intelligence bonus";
	}
}
