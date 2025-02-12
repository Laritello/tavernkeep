using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterDeleted
{
	public class EncounterDeletedNotificationHandler(
		IHubContext<EncounterHub, IEncounterHub> context
		) : INotificationHandler<EncounterDeletedNotification>
	{
		public async Task Handle(EncounterDeletedNotification request, CancellationToken cancellationToken)
		{
			// TODO: Update only for those who's in the encounter
			await context.Clients.All.OnEncounterDeleted(request.Encounter.Id);
		}
	}
}
