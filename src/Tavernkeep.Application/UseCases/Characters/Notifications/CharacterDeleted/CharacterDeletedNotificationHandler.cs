using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Characters.Notifications.CharacterDeleted
{
	internal class CharacterDeletedNotificationHandler(
		IHubContext<CharacterHub, ICharacterHub> context
		) : INotificationHandler<CharacterDeletedNotification>
	{
		public async Task Handle(CharacterDeletedNotification request, CancellationToken cancellationToken)
		{
			await context.Clients.All.OnCharacterDeleted(request.Character.Id);
		}
	}
}
