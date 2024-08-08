using MediatR;

namespace Tavernkeep.Application.UseCases.Chat.Commands.DeleteMessage
{
	public class DeleteMessageCommand(Guid messageId) : IRequest
	{
		public Guid MessageId { get; set; } = messageId;
	}
}
