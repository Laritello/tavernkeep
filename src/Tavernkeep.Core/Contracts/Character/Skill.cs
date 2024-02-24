using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Contracts.Character
{
    public class Skill
    {
        #region Constructors

        public Skill()
        {

        }

        public Skill(Entities.Character owner, SkillType type)
        {
            Owner = owner;
            Type = type;
        }

        #endregion

        #region Properties

        [JsonIgnore]
        public Entities.Character Owner { get; set; } = default!;
        public SkillType Type { get; set; }
        public Proficiency Proficiency { get; set; }
        public int Bonus => Owner.GetSkillAbility(Type).Modifier + Proficiency.GetProficiencyBonus(Owner);

        #endregion
    }
}
