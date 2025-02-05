using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Chat.Notifications.TextMessageSent
{
	public class TextMessageSentNotificationHandler(
		IHubContext<ChatHub, IChatHub> context,
		IMapper mapper
		) : INotificationHandler<TextMessageSentNotification>
	{
		public async Task Handle(TextMessageSentNotification request, CancellationToken cancellationToken)
		{
			var message = mapper.Map<TextMessageDto>(request.Message);

			if (message.Recipient == null)
			{
				await context.Clients.All.OnMessageReceived(message);
			}
			else
			{
				// Notify recipient about the new message
				await context.Clients.Users(message.Recipient!.Id.ToString(), message.Sender.Id.ToString()).OnMessageReceived(message);
			}
		}
	}
}
