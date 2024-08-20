using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Character.Dtos;

namespace Tavernkeep.Infrastructure.Notifications.Hubs
{
	public interface ICharacterHub
	{
		Task OnCharacterEdited(CharacterDto notification);
	}

	public class CharacterHub : Hub<ICharacterHub>
	{
		public async Task SendCharacterEditedNotification(CharacterDto notification)
			=> await Clients.All.OnCharacterEdited(notification);
	}
}
