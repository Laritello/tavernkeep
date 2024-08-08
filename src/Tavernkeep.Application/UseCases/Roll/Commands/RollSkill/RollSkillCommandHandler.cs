using MediatR;
using Tavernkeep.Application.Extensions;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Roll.Commands.RollSkill
{
	public class RollSkillCommandHandler(
		IDiceService diceService,
		IUserRepository userRepository,
		ICharacterRepository characterRepository,
		IMessageRepository messageRepository,
		INotificationService notificationService
		) : IRequestHandler<RollSkillCommand, SkillRollMessage>
	{
		public async Task<SkillRollMessage> Handle(RollSkillCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Initiator with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			var skill = character.GetSkill(request.SkillType);
			var roll = diceService.Roll(bonus: skill.Bonus);

			SkillRollMessage message = new()
			{
				Sender = initiator,
				Created = DateTime.UtcNow,
				RollType = request.RollType,
				Expression = roll.DiceExpression,
				Result = roll.ToRollResult(),
				Skill = skill.AsSnapshot()
			};

			messageRepository.Save(message);

			await messageRepository.CommitAsync(cancellationToken);
			await notificationService.QueueMessageAsync(message, cancellationToken);

			return message;
		}
	}
}
