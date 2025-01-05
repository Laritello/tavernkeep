using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Chat.Commands.SendMessage
{
	public class SendMessageCommandHandler(
		IMessageRepository messageRepository,
		IUserRepository userRepository,
		INotificationService notificationService
		)
		: IRequestHandler<SendMessageCommand, Message>
	{
		public async Task<Message> Handle(SendMessageCommand request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(request.Text))
				throw new BusinessLogicException("Text of the message cannot be empty.");

			var sender = await userRepository.GetDetailsAsync(request.SenderId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Sender with specified ID not found.");

			var recipient = request.RecipientId != null
				? await userRepository.FindAsync(request.RecipientId.Value, cancellationToken: cancellationToken) ?? throw new BusinessLogicException("Recipient with specified id not found.")
				: null;

			TextMessage message = new()
			{
				DisplayName = sender.ActiveCharacter is not null ? sender.ActiveCharacter.Name : sender.Login,
				SenderId = sender.Id,
				Sender = sender,
				RecipientId = recipient?.Id,
				Recipient = recipient,
				Created = DateTime.UtcNow,
				Text = request.Text,
			};

			messageRepository.Save(message);

			await messageRepository.CommitAsync(cancellationToken);
			await notificationService.QueueMessageAsync(message, cancellationToken);

			return message;
		}
	}
}
