namespace Tavernkeep.Core.Contracts.Character.Requests
{
	/// <summary>
	/// Represents a request to edit the health of a character.
	/// </summary>
	public class EditHealthRequest
	{
		/// <summary>
		/// The current hitpoints value of the character.
		/// </summary>
		public int Current { get; set; } = default!;

		/// <summary>
		/// The maximum hitpoints value of the character.
		/// </summary>
		public int Max { get; set; } = default!;

		/// <summary>
		/// The temporary hitpoints value of the character.
		/// </summary>
		public int Temporary { get; set; } = default!;
	}
}
