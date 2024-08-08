using MediatR;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Application.UseCases.Chat.Commands.SendMessage
{
	public class SendMessageCommand(Guid senderId, string content, Guid? recipientId = null) : IRequest<Message>
	{
		public Guid SenderId { get; set; } = senderId;
		public string Text { get; set; } = content;
		public Guid? RecipientId { get; set; } = recipientId;
	}
}
