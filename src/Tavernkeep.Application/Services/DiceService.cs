using d20Tek.DiceNotation;
using d20Tek.DiceNotation.DieRoller;
using Tavernkeep.Application.Extensions;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities.Rolls;

namespace Tavernkeep.Application.Services
{
    public class DiceService : IDiceService
    {
        private readonly Dice _dice = InitializeDice();

        public RollResult Roll(string expression)
        {
            var result = _dice.Roll(expression);
            return result.ToRollResult();
        }

        public RollResult Roll(int bonus = 0, bool advantage = false)
        {
            var request = new DiceRequest(advantage ? 2 : 1, 20, bonus: bonus, choose: 1);
            var result = _dice.Roll(request);
            return result.ToRollResult();
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
