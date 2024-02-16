using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character.Requests
{
    public class EditAbilityRequest
    {
        public Guid CharacterId { get; set; }
        public AbilityType Type { get; set; }
        public int Score { get; set; }
    }
}
