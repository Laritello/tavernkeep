using MediatR;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Chat.Commands.SendMessage
{
    public class SendMessageCommandHandler(IMessageRepository messageRepository, IUserRepository userRepository) : IRequestHandler<SendMessageCommand, Message>
    {
        public async Task<Message> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var sender = await userRepository.FindAsync(request.SenderId)
                ?? throw new BusinessLogicException("Sender with specified id not found");

            Message message = new()
            {
                Sender = sender,
                Created = DateTime.UtcNow,
                Type = request.Type,
                Content = request.Content,
            };

            messageRepository.Save(message);
            await messageRepository.CommitAsync(cancellationToken);

            return message;
        }
    }
}
