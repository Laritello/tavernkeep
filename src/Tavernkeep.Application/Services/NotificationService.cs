using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;
using Tavernkeep.Core.Notifications;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.Services
{
	/// <summary>
	/// Handles the notification's distribution to the clients.
	/// </summary>
	/// <param name="logger">The <see cref="ILogger"/> instance.</param>
	/// <param name="mediator">The <see cref="IMediator"/> instance.</param>
	public class NotificationService(
		ILogger<NotificationService> logger,
		IMediator mediator
		) : BackgroundService, INotificationService
	{
		private readonly Channel<IBaseNotification> _queue = Channel.CreateUnbounded<IBaseNotification>();

		public ValueTask Publish<T>(T notification, CancellationToken cancellationToken) where T : IBaseNotification => _queue.Writer.WriteAsync(notification, cancellationToken);

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				try
				{
					var notification = await _queue.Reader.ReadAsync(cancellationToken);

					if (notification is not INotification)
					{
						throw new InvalidCastException($"The notification must implement {nameof(INotification)} intereface.");
					}

					await mediator.Publish(notification, cancellationToken);
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
