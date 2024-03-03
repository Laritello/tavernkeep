using d20Tek.DiceNotation;
using d20Tek.DiceNotation.DieRoller;
using d20Tek.DiceNotation.Results;
using Tavernkeep.Application.Interfaces;

namespace Tavernkeep.Application.Services
{
    public class DiceService : IDiceService
    {
        private readonly Dice _dice = InitializeDice();

        public DiceResult Roll(string expression)
        {
            var result = _dice.Roll(expression);
            return result;
        }

        public DiceResult Roll(int bonus = 0, bool advantage = false)
        {
            var request = new DiceRequest(advantage ? 2 : 1, 20, bonus: bonus, choose: 1);
            var result = _dice.Roll(request);
            return result;
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
