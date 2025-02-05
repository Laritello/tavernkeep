using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Chat.Notifications.ChatDeleted
{
	public class ChatDeletedNotificationHandler(
		IHubContext<ChatHub, IChatHub> context
		) : INotificationHandler<ChatDeletedNotification>
	{
		public async Task Handle(ChatDeletedNotification request, CancellationToken cancellationToken)
		{
			await context.Clients.All.OnChatDeleted();
		}
	}
}
