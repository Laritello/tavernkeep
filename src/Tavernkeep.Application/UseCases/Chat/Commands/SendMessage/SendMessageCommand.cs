using MediatR;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Application.UseCases.Chat.Commands.SendMessage
{
	public class SendMessageCommand : IRequest<Message>
	{
		public Guid SenderId { get; set; }
		public string Text { get; set; }
		public Guid? RecipientId { get; set; }

		public SendMessageCommand(Guid senderId, string content, Guid? recipientId = null)
		{
			SenderId = senderId;
			Text = content;
			RecipientId = recipientId;
		}
	}
}
