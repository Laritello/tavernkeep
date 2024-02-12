using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Actions.Chat.Commands.SendMessage
{
    public class SendMessageCommand : IRequest<Message>
    {
        public Guid SenderId { get; set; }
        public MessageType Type { get; set; }
        public string Content { get; set; }

        public SendMessageCommand(Guid senderId, MessageType type, string content)
        {
            SenderId = senderId;
            Type = type;
            Content = content;
        }
    }
}
