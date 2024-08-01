using Tavernkeep.Core.Entities.Rolls;
using Tavernkeep.Core.Entities.Snapshots;

namespace Tavernkeep.Core.Entities.Messages
{
    public class SkillRollMessage : RollMessage
    {
        #region Constructors

        public SkillRollMessage() { }

        #endregion

        #region Properties

        public SkillSnapshot Skill { get; set; } = default!;

        #endregion
    }
}
