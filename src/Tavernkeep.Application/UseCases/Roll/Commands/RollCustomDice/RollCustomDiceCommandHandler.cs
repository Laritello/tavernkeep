﻿using AutoMapper;
using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Roll.Commands.RollCustomDice
{
    public class RollCustomDiceCommandHandler
        (
        IDiceService diceService,
        IUserRepository userRepository,
        IMessageRepository messageRepository, 
        INotificationService notificationService,
        IMapper mapper
        ) : IRequestHandler<RollCustomDiceCommand, RollMessageDto>
    {
        public async Task<RollMessageDto> Handle(RollCustomDiceCommand request, CancellationToken cancellationToken)
        {
            var initiator = await userRepository.FindAsync(request.InitiatorId)
                ?? throw new BusinessLogicException("Initiator with specified ID doesn't exist.");

            var result = diceService.Roll(request.Expression);

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

            return mapper.Map<RollMessageDto>(message);
        }
    }
}
