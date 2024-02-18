namespace Tavernkeep.Core.Contracts.Character.Requests
{
    public class ModifyHealthRequest
    {
        public Guid CharacterId { get; set; }
        public int Change { get; set; }
    }
}
