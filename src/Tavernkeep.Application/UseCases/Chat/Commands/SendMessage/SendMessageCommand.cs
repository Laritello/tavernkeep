using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Actions.Chat.Commands.SendMessage
{
    public class SendMessageCommand : IRequest<Message>
    {
        public Guid SenderId { get; set; }
        public string Content { get; set; }
        public Guid? RecipientId { get; set; }

        public SendMessageCommand(Guid senderId, string content, Guid? recipientId = null)
        {
            SenderId = senderId;
            Content = content;
            RecipientId = recipientId;
        }
    }
}
