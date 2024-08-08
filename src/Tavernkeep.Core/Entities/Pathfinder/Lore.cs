using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Entities.Pathfinder
{
    public class Lore
    {
        #region Constructors

        public Lore()
        {

        }

        public Lore(Character owner, string topic)
        {
            Owner = owner;
            Topic = topic;
        }

        #endregion

        #region Properties

        [JsonIgnore]
        public Character Owner { get; set; } = default!;
        public string Topic { get; set; } = default!;
        public Proficiency Proficiency { get; set; }
        public int Bonus => Owner.GetAbility(AbilityType.Intelligence).Modifier + Proficiency.GetProficiencyBonus(Owner);

        #endregion
    }
}
