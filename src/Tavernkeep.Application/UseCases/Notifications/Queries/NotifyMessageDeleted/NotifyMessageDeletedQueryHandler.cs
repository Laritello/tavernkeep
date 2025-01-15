using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyMessageDeleted
{
	public class NotifyMessageDeletedQueryHandler(IHubContext<ChatHub, IChatHub> context) : IRequestHandler<NotifyMessageDeletedQuery>
	{
		public async Task Handle(NotifyMessageDeletedQuery request, CancellationToken cancellationToken)
		{
			await context.Clients.All.DeleteMessage(request.Message);
		}
	}
}
