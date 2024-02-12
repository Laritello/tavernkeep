using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Chat
{
    public class SendMessageRequest
    {
        public MessageType Type { get; set; }
        public string Content { get; set; } = default!;
    }
}
