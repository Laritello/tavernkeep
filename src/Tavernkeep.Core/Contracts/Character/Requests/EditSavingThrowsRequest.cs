using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character.Requests
{
	/// <summary>
	/// Represents a request to edit the saving throws of a character.
	/// </summary>
	public class EditSavingThrowsRequest
	{
		/// <summary>
		/// The updated proficiencies.
		/// </summary>
		public Dictionary<SavingThrowType, Proficiency> Proficiencies { get; set; } = [];
	}
}
