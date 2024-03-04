using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Notifications.Queries.NotifyAbilityEdited;
using Tavernkeep.Application.UseCases.Notifications.Queries.NotifyRollMessage;
using Tavernkeep.Application.UseCases.Notifications.Queries.NotifySkillEdited;
using Tavernkeep.Application.UseCases.Notifications.Queries.NotifyTextMessage;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.Services
{
    /// <summary>
    /// Handles the notification's distribution to the clients.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> instance.</param>
    /// <param name="logger">The <see cref="ILogger"/> instance.</param>
    public class NotificationService(
        IServiceProvider serviceProvider, 
        ILogger<NotificationService> logger,
        IMediator mediator
        ) : BackgroundService, INotificationService
    {
        private readonly Channel<object> _queue = Channel.CreateUnbounded<object>();

        public ValueTask QueueMessage(MessageDto message) =>_queue.Writer.WriteAsync(message);
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

                    // TODO: Switch to singular notification hub structure
                    switch (notification)
                    {
                        case TextMessageDto textMessage:
                            await mediator.Send(new NotifyTextMessageQuery(textMessage), cancellationToken);
                            break;

                        case RollMessageDto rollMessage:
                            await mediator.Send(new NotifyRollMessageQuery(rollMessage), cancellationToken);
                            break;

                        case AbilityEditedNotification abilityNotification:
                            await mediator.Send(new NotifyAbilityEditedQuery(abilityNotification), cancellationToken);
                            break;

                        case SkillEditedNotification skillEditedNotification:
                            await mediator.Send(new NotifySkillEditedQuery(skillEditedNotification), cancellationToken);
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
    }
}
