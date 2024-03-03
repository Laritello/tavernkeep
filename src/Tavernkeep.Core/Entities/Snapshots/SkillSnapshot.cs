using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Snapshots
{
    public class SkillSnapshot
    {
        public SkillType Type { get; set; }
        public Proficiency Proficiency { get; set; }
        public int Bonus { get; set; }
    }
}
