using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Snapshots;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Entities
{
    public class Skill
    {
        #region Constructors

        public Skill()
        {

        }

        public Skill(Character owner, AbilityType baseAbility, SkillType type)
        {
            Owner = owner;
            BaseAbility = baseAbility;
            Type = type;
        }

        #endregion

        #region Properties

        [JsonIgnore]
        public Character Owner { get; set; } = default!;
        public AbilityType BaseAbility { get; set; }
        public SkillType Type { get; set; }
        public Proficiency Proficiency { get; set; }
        public int Bonus => Owner.GetSkillAbility(Type).Modifier + Proficiency.GetProficiencyBonus(Owner);

        #endregion

        #region Methods

        public SkillSnapshot AsSnapshot()
        {
            return new()
            {
                Type = Type,
                Proficiency = Proficiency,
                Bonus = Bonus
            };
        }

        #endregion
    }
}
