using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Infrastructure.Notifications.Hubs
{
	public interface ICharacterHub
	{
		Task OnCharacterEdited(CharacterEditedNotification notification);
	}

	public class CharacterHub : Hub<ICharacterHub>
	{
		public async Task SendCharacterEditedNotification(CharacterEditedNotification notification)
			=> await Clients.All.OnCharacterEdited(notification);
	}
}
