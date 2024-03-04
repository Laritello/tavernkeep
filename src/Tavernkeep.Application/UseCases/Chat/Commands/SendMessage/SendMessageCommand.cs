using MediatR;
using Tavernkeep.Core.Contracts.Chat.Dtos;

namespace Tavernkeep.Application.Actions.Chat.Commands.SendMessage
{
    public class SendMessageCommand : IRequest<MessageDto>
    {
        public Guid SenderId { get; set; }
        public string Text { get; set; }
        public Guid? RecipientId { get; set; }

        public SendMessageCommand(Guid senderId, string content, Guid? recipientId = null)
        {
            SenderId = senderId;
            Text = content;
            RecipientId = recipientId;
        }
    }
}
