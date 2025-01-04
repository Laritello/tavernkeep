using MediatR;
using Tavernkeep.Application.Extensions;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Roll.Commands.RollCustomDice
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
				Sender = initiator,
				Created = DateTime.UtcNow,
				RollType = request.RollType,
				Expression = roll.DiceExpression,
				Result = roll.ToRollResult()
			};

			messageRepository.Save(message);

			await messageRepository.CommitAsync(cancellationToken);
			await notificationService.QueueMessageAsync(message, cancellationToken);

			return message;
		}
	}
}
