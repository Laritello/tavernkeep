using Tavernkeep.Domain.Notifications;

namespace Tavernkeep.Domain.Services
{
	public interface INotificationService
	{
		ValueTask Publish<T>(T notification, CancellationToken cancellationToken) where T : IBaseNotification;
	}
}
