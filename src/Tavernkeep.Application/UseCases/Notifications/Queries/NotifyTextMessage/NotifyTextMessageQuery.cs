using MediatR;
using Tavernkeep.Core.Contracts.Chat.Dtos;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyTextMessage
{
    public class NotifyTextMessageQuery : IRequest
    {
        public TextMessageDto Message { get; set; }

        public NotifyTextMessageQuery(TextMessageDto message)
        {
            Message = message;
        }
    }
}
