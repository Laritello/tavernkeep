using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Domain.Contracts.Character.Requests
{
	/// <summary>
	/// Represents a request to edit the saving throws of a character.
	/// </summary>
	public class EditSavingThrowsRequest
	{
		/// <summary>
		/// The updated proficiencies.
		/// </summary>
		public Dictionary<string, Proficiency> Proficiencies { get; set; } = [];
	}
}
