using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.Interfaces
{
	public interface INotificationService
	{
		ValueTask QueueMessageAsync(Message message, CancellationToken cancellationToken = default);
		ValueTask QueueCharacterNotificationAsync(CharacterEditedNotification notification, CancellationToken cancellationToken = default);
	}
}
