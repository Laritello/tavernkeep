using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Encounters.Dtos;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterCreated
{
	public class EncounterCreatedNotificationHandler(
		IHubContext<EncounterHub, IEncounterHub> context,
		IMapper mapper
		) : INotificationHandler<EncounterCreatedNotification>
	{
		public async Task Handle(EncounterCreatedNotification request, CancellationToken cancellationToken)
		{
			var encounter = mapper.Map<EncounterDto>(request.Encounter);

			// TODO: Update only for those who's in the encounter
			await context.Clients.All.OnEncounterCreated(encounter);
		}
	}
}
