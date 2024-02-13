using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character
{
    public class Ability
    {
        public Entities.Character Owner { get; set; } = default!;
        public AbilityType Type { get; set; }
        public int Score { get; set; }

        public Ability()
        {
             
        }

        public Ability(Entities.Character owner)
        {
            Owner = owner;
        }
    }
}
