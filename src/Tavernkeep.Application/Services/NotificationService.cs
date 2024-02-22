using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Infrastructure.Notifications.Hubs;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.Services
{
    public class NotificationService(IServiceProvider serviceProvider, ILogger<NotificationService> logger) : BackgroundService, INotificationService
    {
        private readonly Channel<object> _channel = Channel.CreateUnbounded<object>();

        public ValueTask PushMessage(Message message) =>_channel.Writer.WriteAsync(message);
        public ValueTask PushAbilityNotification(AbilityEditedNotification notification) =>_channel.Writer.WriteAsync(notification);
        public ValueTask PushSkillNotification(SkillEditedNotification notification) =>_channel.Writer.WriteAsync(notification);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var notification = await _channel.Reader.ReadAsync(stoppingToken);
                    using var scope = serviceProvider.CreateScope();

                    switch (notification)
                    {
                        case TextMessage textMessage:
                            await SendTextMessageNotification(scope, textMessage);
                            break;

                        case AbilityEditedNotification abilityNotification:
                            await SendAbilityNotification(scope, abilityNotification);
                            break;

                        case SkillEditedNotification skillEditedNotification:
                            await SendSkillNotification(scope, skillEditedNotification);
                            break;
                        default:
                            throw new NotImplementedException($"Notification implementation for {nameof(notification)} doesn't exist.");
                    };
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Error in notification service");
                }
            }
        }

        private async Task SendTextMessageNotification(IServiceScope scope, TextMessage message)
        {
            var context = scope.ServiceProvider.GetRequiredService<IHubContext<ChatHub, IChatHub>>();

            if (message.Recipient == null)
            {
                // Notify all connected users about the new message
                await context.Clients.All.ReceiveMessage(message);
            }
            else
            {
                // Notify participants about the new message
                List<string> recipients = [message.SenderId.ToString(), message.RecipientId!.Value.ToString()];
                await context.Clients.Users(recipients).ReceiveMessage(message);
            }
        }

        private async Task SendAbilityNotification(IServiceScope scope, AbilityEditedNotification notification)
        {
            var context = scope.ServiceProvider.GetRequiredService<IHubContext<CharacterHub, ICharacterHub>>();
            await context.Clients.All.OnAbilityEdited(notification);
        }

        private async Task SendSkillNotification(IServiceScope scope, SkillEditedNotification notification)
        {
            var context = scope.ServiceProvider.GetRequiredService<IHubContext<CharacterHub, ICharacterHub>>();
            await context.Clients.All.OnSkillEdited(notification);
        }
    }
}
