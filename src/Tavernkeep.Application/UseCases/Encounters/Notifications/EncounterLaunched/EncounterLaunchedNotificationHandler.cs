using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterLaunched
{
	public class EncounterLaunchedNotificationHandler(
		IHubContext<EncounterHub, IEncounterHub> context
		) : INotificationHandler<EncounterLaunchedNotification>
	{
		public async Task Handle(EncounterLaunchedNotification request, CancellationToken cancellationToken)
		{
			// TODO: Update only for those who's in the encounter
			await context.Clients.All.OnEncounterLaunched(request.Encounter.Id);
		}
	}
}
