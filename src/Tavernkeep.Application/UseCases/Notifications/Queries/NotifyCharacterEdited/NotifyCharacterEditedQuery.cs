using MediatR;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyCharacterEdited
{
	public class NotifyCharacterEditedQuery : IRequest
	{
		public CharacterEditedNotification Notification { get; set; }

		public NotifyCharacterEditedQuery(CharacterEditedNotification notification)
		{
			Notification = notification;
		}
	}
}
