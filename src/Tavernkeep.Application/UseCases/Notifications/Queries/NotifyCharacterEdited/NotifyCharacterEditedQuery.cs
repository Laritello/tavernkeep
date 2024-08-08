using MediatR;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyCharacterEdited
{
	public class NotifyCharacterEditedQuery(CharacterEditedNotification notification) : IRequest
	{
		public CharacterEditedNotification Notification { get; set; } = notification;
	}
}
