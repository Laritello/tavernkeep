using AutoMapper;
using MediatR;
using Tavernkeep.Application.Extensions;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Roll.Commands.RollSkill
{
    public class RollSkillCommandHandler
        (
        IUserRepository userRepository, 
        ICharacterRepository characterRepository, 
        IMessageRepository messageRepository,
        IDiceService diceService,
        INotificationService notificationService,
        IMapper mapper
        ) : IRequestHandler<RollSkillCommand, SkillRollMessageDto>
    {
        public async Task<SkillRollMessageDto> Handle(RollSkillCommand request, CancellationToken cancellationToken)
        {
            var initiator = await userRepository.FindAsync(request.InitiatorId)
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
            
            var result = mapper.Map<SkillRollMessageDto>(message);
            await notificationService.QueueMessage(result);

            return result;
        }
    }
}
