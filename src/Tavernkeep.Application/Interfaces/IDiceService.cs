using d20Tek.DiceNotation.Results;
using Tavernkeep.Core.Entities.Rolls;

namespace Tavernkeep.Application.Interfaces
{
    public interface IDiceService
    {
        public DiceResult Roll(string diceNotation);
        public DiceResult Roll(int bonus = 0, bool advantage = false);
    }
}
