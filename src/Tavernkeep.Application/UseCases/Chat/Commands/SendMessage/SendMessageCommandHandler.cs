using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Chat.Commands.SendMessage
{
    public class SendMessageCommandHandler
        (
        IMessageRepository messageRepository, 
        IUserRepository userRepository,
        INotificationService notificationService,
        IMapper mapper
        ) 
        : IRequestHandler<SendMessageCommand, MessageDto>
    {
        public async Task<MessageDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var sender = await userRepository.FindAsync(request.SenderId)
                ?? throw new BusinessLogicException("Sender with specified id not found");

            var recipient = request.RecipientId != null ? await userRepository.FindAsync(request.RecipientId.Value) : null;

            TextMessage message = new()
            {
                Sender = sender,
                Recipient = recipient,
                Created = DateTime.UtcNow,
                Text = request.Text,
            };

            messageRepository.Save(message);

            await messageRepository.CommitAsync(cancellationToken);
            await notificationService.QueueMessage(message);

            return mapper.Map<MessageDto>(message);
        }
    }
}
