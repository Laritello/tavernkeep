using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Infrastructure.Notifications.Hubs;
using Tavernkeep.Infrastructure.Notifications.Storage;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyTextMessage
{
    public class NotifyTextMessageQueryHandler(
        IUserConnectionStorage<Guid> userStorage, 
        IHubContext<ChatHub, IChatHub> context
        ) : IRequestHandler<NotifyTextMessageQuery>
    {
        public async Task Handle(NotifyTextMessageQuery request, CancellationToken cancellationToken)
        {
            var message = request.Message;

            if (message.Recipient == null)
            {
                // Notify all connected recipients about the new message
                var senderConnections = userStorage.GetConnections(message.Sender.Id);
                await context.Clients.AllExcept(senderConnections).ReceiveMessage(message);
            }
            else
            {
                // Notify recipient about the new message
                await context.Clients.User(message.Recipient!.Id.ToString()).ReceiveMessage(message);
            }
        }
    }
}
