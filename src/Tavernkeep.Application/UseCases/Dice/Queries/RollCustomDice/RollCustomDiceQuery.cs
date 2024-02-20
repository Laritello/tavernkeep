using MediatR;

namespace Tavernkeep.Application.UseCases.Dice.Queries.RollCustomDice
{
    public class RollCustomDiceQuery : IRequest<int>
    {
        public string Expression { get; set; }

        public RollCustomDiceQuery(string expression)
        {
            Expression = expression;
        }
    }
}
