using MediatR;
using System.Reflection;
using Tavernkeep.Application.Extensions;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Chat.Notifications.RollMessageSent;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Services;

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

			var skill = character.Skills[request.SkillType];
			var roll = diceService.Roll(bonus: skill.Bonus);

			SkillRollMessage message = new()
			{
				CharacterId = character.Id,
				DisplayName = initiator.ActiveCharacter is not null ? initiator.ActiveCharacter.Name : initiator.Login,
				Sender = initiator,
				Created = DateTime.UtcNow,
				RollType = request.RollType,
				Expression = roll.DiceExpression,
				Result = roll.ToRollResult(),
				Skill = skill.AsSnapshot()
			};

			messageRepository.Save(message);

			await messageRepository.CommitAsync(cancellationToken);
			await notificationService.Publish(new RollMessageSentNotification(message), cancellationToken);

			return message;
		}
	}
}
