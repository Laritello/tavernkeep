using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Infrastructure.Notifications.Hubs
{
    public interface IChatHub
    {
        Task ReceiveMessage(Message message);
    }

    public class ChatHub(ILogger<ChatHub> logger) : Hub<IChatHub>
    {
        public async Task SendMessageNotification(Message message) => await Clients.All.ReceiveMessage(message);
    }
}
