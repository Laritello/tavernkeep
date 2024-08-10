using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character.Requests
{
	/// <summary>
	/// Represents a request to edit the skill of a character.
	/// </summary>
	public class EditSkillRequest
	{
		/// <summary>
		/// The ID of the character.
		/// </summary>
		public Guid CharacterId { get; set; } = default!;

		/// <summary>
		/// The type of the skill.
		/// </summary>
		public SkillType Type { get; set; } = default!;

		/// <summary>
		/// The proficiency of the skill.
		/// </summary>
		public Proficiency Proficiency { get; set; } = default!;
	}
}
