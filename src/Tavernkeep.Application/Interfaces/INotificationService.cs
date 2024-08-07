using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.Interfaces
{
    public interface INotificationService
    {
        ValueTask QueueMessage(Message message);
        ValueTask QueueCharacterNotification(CharacterEditedNotification notification);
    }
}
