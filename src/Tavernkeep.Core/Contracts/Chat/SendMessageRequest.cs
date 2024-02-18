namespace Tavernkeep.Core.Contracts.Chat
{
    public class SendMessageRequest
    {
        public Guid? RecipientId { get; set; }
        public string Content { get; set; } = default!;
    }
}
