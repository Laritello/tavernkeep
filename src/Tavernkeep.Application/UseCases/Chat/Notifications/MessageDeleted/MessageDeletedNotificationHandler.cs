using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Chat.Notifications.MessageDeleted
{
	public class MessageDeletedNotificationHandler(
		IHubContext<ChatHub, IChatHub> context,
		IMapper mapper
		) : INotificationHandler<MessageDeletedNotification>
	{
		public async Task Handle(MessageDeletedNotification request, CancellationToken cancellationToken)
		{
			var dto = mapper.Map<MessageDeletedDto>(request.Message);
			await context.Clients.All.DeleteMessage(dto);
		}
	}
}
