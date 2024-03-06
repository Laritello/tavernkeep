using MediatR;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyRollMessage
{
    public class NotifyRollMessageQuery : IRequest
    {
        public RollMessage Message { get; set; }

        public NotifyRollMessageQuery(RollMessage message)
        {
            Message = message;
        }
    }
}
