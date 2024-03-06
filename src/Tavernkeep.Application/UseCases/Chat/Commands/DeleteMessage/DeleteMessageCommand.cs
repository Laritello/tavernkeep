using MediatR;

namespace Tavernkeep.Application.UseCases.Chat.Commands.DeleteMessage
{
    public class DeleteMessageCommand : IRequest
    {
        public Guid MessageId { get; set; }

        public DeleteMessageCommand(Guid messageId)
        {
            MessageId = messageId;
        }
    }
}
