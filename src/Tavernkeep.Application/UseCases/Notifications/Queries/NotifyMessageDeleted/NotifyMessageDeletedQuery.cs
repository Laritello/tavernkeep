using MediatR;
using Tavernkeep.Core.Contracts.Chat.Dtos;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyMessageDeleted
{
	public class NotifyMessageDeletedQuery(MessageDeletedDto message) : IRequest
	{
		public MessageDeletedDto Message { get; set; } = message;
	}
}
