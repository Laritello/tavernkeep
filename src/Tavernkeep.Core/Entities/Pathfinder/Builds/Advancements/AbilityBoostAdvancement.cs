using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements
{
	/// <summary>
	/// Advancement that increases selected ability score.
	/// </summary>
	public class AbilityBoostAdvancement : Advancement
	{
		/// <summary>
		/// List of possible targets abilities for the boost.
		/// </summary>
		public List<AbilityType> Possible { get; set; } = [];

		/// <summary>
		/// Selected target ability of the boost.
		/// </summary>
		public AbilityType Selected { get; set; }

		public override string ToString() => $"AbilityBoostAdvancement: {Selected} ({string.Join(',', Possible)})";
	}
}
