using MediatR;
using Tavernkeep.Application.Interfaces;

namespace Tavernkeep.Application.UseCases.Dice.Queries.RollCustomDice
{
    public class RollCustomDiceQueryHandler(IDiceService diceService) : IRequestHandler<RollCustomDiceQuery, int>
    {
        public Task<int> Handle(RollCustomDiceQuery request, CancellationToken cancellationToken)
        {
            var result = diceService.Roll(request.Expression);
            return Task.FromResult(result);
        }
    }
}
