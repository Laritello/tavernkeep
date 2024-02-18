namespace Tavernkeep.Core.Contracts.Chat.Requests
{
    /// <summary>
    /// Represents a request to send a message.
    /// </summary>
    public class SendMessageRequest
    {
        /// <summary>
        /// The ID of the message recipient. If ID isn't provided message considered public.
        /// </summary>
        public Guid? RecipientId { get; set; } = default!;

        /// <summary>
        /// The content of the message.
        /// </summary>
        public string Content { get; set; } = default!;
    }
}
