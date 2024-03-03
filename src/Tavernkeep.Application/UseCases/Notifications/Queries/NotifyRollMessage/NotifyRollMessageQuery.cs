using MediatR;
using Tavernkeep.Core.Contracts.Chat.Dtos;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyRollMessage
{
    public class NotifyRollMessageQuery : IRequest
    {
        public RollMessageDto Message { get; set; }

        public NotifyRollMessageQuery(RollMessageDto message)
        {
            Message = message;
        }
    }
}
