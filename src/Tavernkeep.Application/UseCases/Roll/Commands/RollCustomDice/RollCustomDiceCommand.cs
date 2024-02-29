using MediatR;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Application.UseCases.Roll.Commands.RollCustomDice
{
    public class RollCustomDiceCommand : IRequest<RollMessageDto>
    {
        public Guid InitiatorId { get; set; }
        public RollType RollType { get; set; }
        public string Expression { get; set; }

        public RollCustomDiceCommand(Guid initiatorId, RollType rollType, string expression)
        {
            InitiatorId = initiatorId;
            RollType = rollType;
            Expression = expression;
        }
    }
}
