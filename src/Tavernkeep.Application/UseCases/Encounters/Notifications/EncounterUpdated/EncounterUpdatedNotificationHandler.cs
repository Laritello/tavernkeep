using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Encounters.Dtos;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterUpdated
{
	public class EncounterUpdatedNotificationHandler(
		IHubContext<EncounterHub, IEncounterHub> context,
		IMapper mapper
		) : INotificationHandler<EncounterUpdatedNotification>
	{
		public async Task Handle(EncounterUpdatedNotification request, CancellationToken cancellationToken)
		{
			var encounter = mapper.Map<EncounterDto>(request.Encounter);

			// TODO: Update only for those who's in the encounter
			await context.Clients.All.OnEncounterUpdated(encounter);
		}
	}
}
