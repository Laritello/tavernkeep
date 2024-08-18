using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyCharacterEdited
{
	public class NotifyCharacterEditedQueryHandler(IHubContext<CharacterHub, ICharacterHub> context) : IRequestHandler<NotifyCharacterEditedQuery>
	{
		public async Task Handle(NotifyCharacterEditedQuery request, CancellationToken cancellationToken)
		{
			await context.Clients.All.OnCharacterEdited(request.Notification);
		}
	}
}
