﻿using MediatR;
using Tavernkeep.Application.Interfaces;
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
        INotificationService notificationService
        ) : IRequestHandler<RollSkillCommand, RollMessage>
    {
        public async Task<RollMessage> Handle(RollSkillCommand request, CancellationToken cancellationToken)
        {
            var initiator = await userRepository.FindAsync(request.InitiatorId)
                ?? throw new BusinessLogicException("Initiator with specified ID doesn't exist.");

            var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
                ?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

            var result = diceService.Roll(bonus: character.GetSkill(request.SkillType).Bonus);


            RollMessage message = new()
            {
                Sender = initiator,
                Created = DateTime.UtcNow,
                RollType = request.RollType,
                Result = result,
            };

            messageRepository.Save(message);

            await messageRepository.CommitAsync(cancellationToken);
            await notificationService.QueueMessage(message);

            return message;
        }
    }
}
