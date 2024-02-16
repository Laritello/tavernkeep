using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character.Requests
{
    public class EditSkillRequest
    {
        public Guid CharacterId { get; set; }
        public SkillType Type { get; set; }
        public Proficiency Proficiency { get; set; }
    }
}
