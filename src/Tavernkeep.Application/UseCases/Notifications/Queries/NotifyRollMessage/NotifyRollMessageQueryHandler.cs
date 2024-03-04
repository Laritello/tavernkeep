using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Infrastructure.Notifications.Hubs;
using Tavernkeep.Infrastructure.Notifications.Storage;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyRollMessage
{
    public class NotifyRollMessageQueryHandler(
        IUserConnectionStorage<Guid> userStorage,
        IHubContext<ChatHub, IChatHub> context
        ) : IRequestHandler<NotifyRollMessageQuery>
    {
        public async Task Handle(NotifyRollMessageQuery request, CancellationToken cancellationToken)
        {
            var message = request.Message;

            switch (message.RollType)
            {
                case RollType.Public:
                case RollType.Secret:
                    // Notify all connected recipients about the new message
                    await context.Clients.AllExcept([message.Sender.Id.ToString()]).ReceiveMessage(message);
                    break;

                case RollType.Private:
                    // Notify sender about the new message
                    var senderConnections = userStorage.GetConnections(message.Sender.Id);
                    await context.Clients.Clients(senderConnections).ReceiveMessage(message);
                    break;
            }
        }
    }
}
