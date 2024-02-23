namespace Tavernkeep.Application.Interfaces
{
    public interface IDiceService
    {
        public int Roll(string diceNotation);
        public int Roll(int bonus = 0, bool advantage = false);
    }
}
