using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character.Requests
{
	/// <summary>
	/// Represents a request to edit the perception of a character.
	/// </summary>
	public class EditPerceptionRequest
	{
		/// <summary>
		/// The ID of the character.
		/// </summary>
		public Guid CharacterId { get; set; } = default!;

		/// <summary>
		/// The proficiency of the perception.
		/// </summary>
		public Proficiency Proficiency { get; set; } = default!;
	}
}
