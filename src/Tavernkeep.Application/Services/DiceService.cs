using d20Tek.DiceNotation;
using d20Tek.DiceNotation.DieRoller;
using Tavernkeep.Application.Interfaces;

namespace Tavernkeep.Application.Services
{
    public class DiceService : IDiceService
    {
        private readonly Dice _dice = InitializeDice();

        public int Roll(string expression)
        {
            var result = _dice.Roll(expression, new CryptoDieRoller());
            return result.Value;
        }

        private static Dice InitializeDice()
        {
            var dice = new Dice();

            dice.Configuration.HasBoundedResult = false;
            dice.Configuration.DefaultDieSides = 20;
            dice.Configuration.DefaultDieRoller = new CryptoDieRoller();

            return dice;
        }
    }
}
