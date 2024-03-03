namespace Tavernkeep.Core.Entities.Rolls
{
    public class RollResult
    {
        public int Value { get; set; }
        public int Modifier { get; set; }
        public List<ThrowResult> Results { get; set; }

        public RollResult() 
        {
            Results = [];
        }
    }
}
