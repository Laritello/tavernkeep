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
		/// The ID of the character.
		/// </summary>
		public Guid CharacterId { get; set; } = default!;

		/// <summary>
		/// The change in character's health in hitpoints.
		/// </summary>
		public int Change { get; set; } = default!;
	}
}
