using MediatR;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifySkillEdited
{
    public class NotifySkillEditedQuery : IRequest
    {
        public SkillEditedNotification Notification { get; set; }

        public NotifySkillEditedQuery(SkillEditedNotification notification)
        {
            Notification = notification;
        }
    }
}
