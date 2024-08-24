using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements
{
	/// <summary>
	/// Advancement that decreases selected ability score.
	/// </summary>
	public class AbilityFlawAdvancement : Advancement
	{
		/// <summary>
		/// List of possible targets abilities for the flaw.
		/// </summary>
		public List<AbilityType> Possible { get; set; } = [];

		/// <summary>
		/// Selected target ability of the flaw.
		/// </summary>
		public AbilityType Selected { get; set; }

		public override string ToString() => $"AbilityFlawAdvancement [Level {Level}]: {Selected}";
	}
}
