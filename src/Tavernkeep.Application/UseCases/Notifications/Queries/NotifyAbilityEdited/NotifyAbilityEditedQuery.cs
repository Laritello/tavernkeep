using MediatR;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyAbilityEdited
{
    public class NotifyAbilityEditedQuery : IRequest
    {
        public AbilityEditedNotification Notification { get; set; }

        public NotifyAbilityEditedQuery(AbilityEditedNotification notification)
        {
            Notification = notification;
        }
    }
}
