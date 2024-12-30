namespace Tavernkeep.Core.Contracts.Character.Requests
{
	/// <summary>
	/// Represents a request to create a character.
	/// </summary>
	public class CreateCharacterRequest
	{
		/// <summary>
		/// The ID of the owner.
		/// </summary>
		public Guid OwnerId { get; set; }

		/// <summary>
		/// The name of the character.
		/// </summary>
		public string Name { get; set; } = default!;

		public string AncestryId { get; set; } = default!;
		public string BackgroundId { get; set; } = default!;
		public string ClassId { get; set; } = default!;
	}
}
