using MediatR;

namespace Tavernkeep.Application.UseCases.Roll.Commands.RollCustomDice
{
    public class RollCustomDiceCommand : IRequest<int>
    {
        public string Expression { get; set; }

        public RollCustomDiceCommand(string expression)
        {
            Expression = expression;
        }
    }
}
