using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Infrastructure.Notifications.Hubs;
using Tavernkeep.Infrastructure.Notifications.Storage;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyTextMessage
{
    public class NotifyTextMessageQueryHandler(
        IHubContext<ChatHub, IChatHub> context
        ) : IRequestHandler<NotifyTextMessageQuery>
    {
        public async Task Handle(NotifyTextMessageQuery request, CancellationToken cancellationToken)
        {
            var message = request.Message;

            if (message.Recipient == null)
            {
                await context.Clients.All.ReceiveMessage(message);
            }
            else
            {
                // Notify recipient about the new message
                await context.Clients.Users(message.Recipient!.Id.ToString(), message.Sender.Id.ToString()).ReceiveMessage(message);
            }
        }
    }
}
