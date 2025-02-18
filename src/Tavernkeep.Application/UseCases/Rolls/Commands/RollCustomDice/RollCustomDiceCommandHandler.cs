using MediatR;
using Tavernkeep.Application.Extensions;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Chat.Notifications.RollMessageSent;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Rolls.Commands.RollCustomDice
{
	public class RollCustomDiceCommandHandler(
		IDiceService diceService,
		IUserRepository userRepository,
		IMessageRepository messageRepository,
		INotificationService notificationService
		) : IRequestHandler<RollCustomDiceCommand, RollMessage>
	{
		public async Task<RollMessage> Handle(RollCustomDiceCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.GetDetailsAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Initiator with specified ID doesn't exist.");

			var roll = diceService.Roll(request.Expression);

			RollMessage message = new()
			{
				CharacterId = initiator.ActiveCharacter?.Id,
				DisplayName = initiator.ActiveCharacter is not null ? initiator.ActiveCharacter.Name : initiator.Login,
				Sender = initiator,
				Created = DateTime.UtcNow,
				RollType = request.RollType,
				Expression = roll.DiceExpression,
				Result = roll.ToRollResult()
			};

			messageRepository.Save(message);

			await messageRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new RollMessageSentNotification(message), cancellationToken);

			return message;
		}
	}
}
