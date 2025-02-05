using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Chat.Notifications.MessageDeleted
{
	public class MessageDeletedNotificationHandler(
		IHubContext<ChatHub, IChatHub> context
		) : INotificationHandler<MessageDeletedNotification>
	{
		public async Task Handle(MessageDeletedNotification request, CancellationToken cancellationToken)
		{
			await context.Clients.All.OnMessageDeleted(request.Message.Id);
		}
	}
}
