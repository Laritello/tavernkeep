namespace Tavernkeep.Core.Contracts.Character.Requests
{
    public class AssignUserRequest
    {
        public Guid CharacterId { get; set; }
        public Guid UserId { get; set; }
    }
}
