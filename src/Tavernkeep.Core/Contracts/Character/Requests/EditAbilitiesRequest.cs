namespace Tavernkeep.Core.Contracts.Character.Requests
{
	/// <summary>
	/// Represents a request to edit the ability of a character.
	/// </summary>
	public class EditAbilitiesRequest
	{
		/// <summary>
		/// The updated scores.
		/// </summary>
		public Dictionary<string, int> Scores { get; set; } = default!;
	}
}
