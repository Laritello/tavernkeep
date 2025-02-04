using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Characters.Notifications.CharacterEdited
{
	public class CharacterEditedNotificationHandler(
		IHubContext<CharacterHub, ICharacterHub> context,
		IMapper mapper
		) : INotificationHandler<CharacterEditedNotification>
	{
		public async Task Handle(CharacterEditedNotification request, CancellationToken cancellationToken)
		{
			var character = mapper.Map<CharacterDto>(request.Character);
			await context.Clients.All.OnCharacterEdited(character);
		}
	}
}
