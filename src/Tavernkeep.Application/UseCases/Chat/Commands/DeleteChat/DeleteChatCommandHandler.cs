using MediatR;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Chat.Commands.DeleteChat
{
    public class DeleteChatCommandHandler(IMessageRepository messageRepository) : IRequestHandler<DeleteChatCommand>
    {
        public async Task Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            await messageRepository.PurgeMessagesAsync(cancellationToken);
        }
    }
}
