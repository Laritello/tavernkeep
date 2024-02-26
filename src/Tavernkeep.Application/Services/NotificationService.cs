using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Infrastructure.Notifications.Hubs;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.Services
{
    /// <summary>
    /// Handles the notification's distribution to the clients.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> instance.</param>
    /// <param name="logger">The <see cref="ILogger"/> instance.</param>
    public class NotificationService(IServiceProvider serviceProvider, ILogger<NotificationService> logger) : BackgroundService, INotificationService
    {
        private readonly Channel<object> _queue = Channel.CreateUnbounded<object>();

        public ValueTask QueueMessage(Message message) =>_queue.Writer.WriteAsync(message);
        public ValueTask QueueAbilityNotification(AbilityEditedNotification notification) =>_queue.Writer.WriteAsync(notification);
        public ValueTask QueueSkillNotification(SkillEditedNotification notification) =>_queue.Writer.WriteAsync(notification);

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var notification = await _queue.Reader.ReadAsync(cancellationToken);
                    using var scope = serviceProvider.CreateScope();

                    // TODO: Implement handlers that encapsulate each specific notification logic
                    // TODO: Switch to singular notification hub structure
                    switch (notification)
                    {
                        case TextMessage textMessage:
                            await SendTextMessageNotification(scope, textMessage);
                            break;

                        case RollMessage rollMessage:
                            await SendRollMessageNotification(scope, rollMessage);
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

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Notification Hosted Service is launching.");
            await base.StartAsync(cancellationToken);
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Notification Hosted Service is stopping.");
            await base.StopAsync(cancellationToken);
        }

        private async Task SendTextMessageNotification(IServiceScope scope, TextMessage message)
        {
            var context = scope.ServiceProvider.GetRequiredService<IHubContext<ChatHub, IChatHub>>();

            if (message.Recipient == null)
            {
                // Notify all connected recipients about the new message
                await context.Clients.AllExcept([message.SenderId.ToString()]).ReceiveMessage(message);
            }
            else
            {
                // Notify recipient about the new message
                await context.Clients.User(message.RecipientId!.Value.ToString()).ReceiveMessage(message);
            }
        }

        private async Task SendRollMessageNotification(IServiceScope scope, RollMessage message)
        {
            var context = scope.ServiceProvider.GetRequiredService<IHubContext<ChatHub, IChatHub>>();

            if (message.RollType == RollType.Public || message.RollType == RollType.Secret)
            {
                // Notify all connected recipients about the new message
                await context.Clients.AllExcept([message.SenderId.ToString()]).ReceiveMessage(message);
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
