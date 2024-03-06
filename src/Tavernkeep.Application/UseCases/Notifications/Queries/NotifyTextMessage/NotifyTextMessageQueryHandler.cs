using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyTextMessage
{
    public class NotifyTextMessageQueryHandler(
        IHubContext<ChatHub, IChatHub> context,
        IMapper mapper
        ) : IRequestHandler<NotifyTextMessageQuery>
    {
        public async Task Handle(NotifyTextMessageQuery request, CancellationToken cancellationToken)
        {
            var message = mapper.Map<TextMessageDto>(request.Message);

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
