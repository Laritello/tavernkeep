using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Domain.Contracts.Encounters.Dtos;

namespace Tavernkeep.Infrastructure.Notifications.Hubs
{
	public interface IEncounterHub
	{
		Task OnEncounterCreated(EncounterDto encounter);
		Task OnEncounterUpdated(EncounterDto encounter);
		Task OnEncounterDeleted(Guid encounterId);
		Task OnEncounterLaunched(Guid encounterId);
	}

	public class EncounterHub : Hub<IEncounterHub> { }
}
