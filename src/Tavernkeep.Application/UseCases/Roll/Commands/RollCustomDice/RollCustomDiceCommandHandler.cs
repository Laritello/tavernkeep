using MediatR;
using Tavernkeep.Application.Interfaces;

namespace Tavernkeep.Application.UseCases.Roll.Commands.RollCustomDice
{
    public class RollCustomDiceCommandHandler(IDiceService diceService) : IRequestHandler<RollCustomDiceCommand, int>
    {
        public Task<int> Handle(RollCustomDiceCommand request, CancellationToken cancellationToken)
        {
            var result = diceService.Roll(request.Expression);
            return Task.FromResult(result);
        }
    }
}
