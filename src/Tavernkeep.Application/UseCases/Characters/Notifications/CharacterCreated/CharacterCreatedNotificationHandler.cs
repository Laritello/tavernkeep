using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Characters.Notifications.CharacterCreated
{
	public class CharacterCreatedNotificationHandler(
		IHubContext<CharacterHub, ICharacterHub> context,
		IMapper mapper
		) : INotificationHandler<CharacterCreatedNotification>
	{
		public async Task Handle(CharacterCreatedNotification request, CancellationToken cancellationToken)
		{
			var character = mapper.Map<CharacterDto>(request.Character);
			await context.Clients.All.OnCharacterCreated(character);
		}
	}
}
