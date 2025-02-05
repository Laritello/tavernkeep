using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Infrastructure.Notifications.Storage;

namespace Tavernkeep.Infrastructure.Notifications.Hubs
{
	public interface IChatHub
	{
		Task OnMessageReceived(MessageDto message);
		Task OnMessageDeleted(Guid messageId);
		Task OnChatDeleted();
	}

	[Authorize]
	public class ChatHub([FromServices] IUserConnectionStorage<Guid> userStorage) : BaseHub<IChatHub>(userStorage) { }
}
