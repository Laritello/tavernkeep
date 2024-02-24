using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Infrastructure.Notifications.Hubs
{
    public interface IChatHub
    {
        Task ReceiveMessage(Message message);
    }

    public class ChatHub : Hub<IChatHub>
    {
        public async Task SendMessageNotification(Message message) => await Clients.All.ReceiveMessage(message);
    }
}
