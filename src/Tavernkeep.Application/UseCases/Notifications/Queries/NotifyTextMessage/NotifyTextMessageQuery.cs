using MediatR;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyTextMessage
{
    public class NotifyTextMessageQuery : IRequest
    {
        public TextMessage Message { get; set; }

        public NotifyTextMessageQuery(TextMessage message)
        {
            Message = message;
        }
    }
}
