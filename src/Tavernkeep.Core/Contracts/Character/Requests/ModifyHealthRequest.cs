namespace Tavernkeep.Core.Contracts.Character.Requests
{
	/// <summary>
	/// Represents a request to modify the health of a character.
	/// </summary>
	/// <remarks>
	/// By modify, we mean healing or causing damage.
	/// </remarks>
	public class ModifyHealthRequest
	{
		/// <summary>
		/// The change in character's health in hitpoints.
		/// </summary>
		public int Change { get; set; } = default!;
	}
}
