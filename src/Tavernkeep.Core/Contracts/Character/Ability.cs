using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character
{
    public class Ability
    {
        #region Constructors

        public Ability()
        {

        }

        public Ability(Entities.Character owner, AbilityType type)
        {
            Owner = owner;
            Type = type;
        }

        #endregion

        #region Properties

        [JsonIgnore]
        public Entities.Character Owner { get; set; } = default!;
        public AbilityType Type { get; set; }
        public int Score { get; set; }

        #endregion
    }
}
