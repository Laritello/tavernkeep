using Tavernkeep.Core.Notifications;

namespace Tavernkeep.Core.Services
{
	public interface INotificationService
	{
		ValueTask Publish<T>(T notification, CancellationToken cancellationToken) where T : IBaseNotification;
	}
}
