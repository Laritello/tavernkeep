using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character.Requests
{
	/// <summary>
	/// Represents a request to create a custom skill.
	/// </summary>
	public class AddCustomSkillRequest
	{
		/// <summary>
		/// The name of the skill.
		/// </summary>
		public required string Name { get; set; }

		/// <summary>
		/// The name of the base ability
		/// </summary>
		public string? BaseAbility { get; set; }

		/// <summary>
		/// The type of the skill.
		/// </summary>
		public required SkillType Type { get; set; }
	}
}
