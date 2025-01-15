namespace Tavernkeep.Core.Contracts.Chat.Dtos
{
	/// <summary>
	/// Used to send notification about deleted messages.
	/// </summary>
	public class MessageDeletedDto
	{
		/// <summary>
		/// ID of deleted message.
		/// </summary>
		public Guid Id { get; set; }
	}
}
