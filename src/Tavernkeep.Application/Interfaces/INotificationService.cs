using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.Interfaces
{
    public interface INotificationService
    {
        ValueTask QueueMessage(MessageDto message);
        ValueTask QueueAbilityNotification(AbilityEditedNotification notification);
        ValueTask QueueSkillNotification(SkillEditedNotification notification);
    }
}
