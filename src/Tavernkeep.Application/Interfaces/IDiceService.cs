using Tavernkeep.Core.Entities.Rolls;

namespace Tavernkeep.Application.Interfaces
{
    public interface IDiceService
    {
        public RollResult Roll(string diceNotation);
        public RollResult Roll(int bonus = 0, bool advantage = false);
    }
}
