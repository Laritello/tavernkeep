using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Notifications.Hubs;

namespace Tavernkeep.Application.Actions.Chat.Commands.SendMessage
{
    public class SendMessageCommandHandler
        (
        IMessageRepository messageRepository, 
        IUserRepository userRepository,
        IHubContext<ChatHub, IChatHub> context
        ) 
        : IRequestHandler<SendMessageCommand, Message>
    {
        public async Task<Message> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var sender = await userRepository.FindAsync(request.SenderId)
                ?? throw new BusinessLogicException("Sender with specified id not found");

            var recipient = request.RecipientId != null ? await userRepository.FindAsync(request.RecipientId.Value) : null;

            Message message = new()
            {
                Sender = sender,
                Recipient = recipient,
                Created = DateTime.UtcNow,
                Type = MessageType.Text,
                Content = request.Content,
            };

            messageRepository.Save(message);
            await messageRepository.CommitAsync(cancellationToken);

            if (message.Recipient == null)
            {
                // Notify all connected users about the new message
                await context.Clients.All.ReceiveMessage(message);
            }
            else
            {
                // Notify participants about the new message
                List<string> recipients = [message.SenderId.ToString(), message.RecipientId!.Value.ToString()];
                await context.Clients.Users(recipients).ReceiveMessage(message);
            }

            return message;
        }
    }
}
