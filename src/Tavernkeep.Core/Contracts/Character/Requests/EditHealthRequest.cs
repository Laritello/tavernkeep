namespace Tavernkeep.Core.Contracts.Character.Requests
{
    public class EditHealthRequest
    {
        public Guid CharacterId { get; set; }
        public int Current { get; set; }
        public int Max { get; set; }
        public int Temporary { get; set; }
    }
}
