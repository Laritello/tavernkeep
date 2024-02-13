using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Character;
using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities
{
    [Table("Characters")]
    public class Character : Entity
    {
        #region Constructors

        public Character()
        {
            Strength = new(this);
            Dexterity = new(this);
            Intelligence = new(this);
            Constitution = new(this);
            Wisdom = new(this);
            Charisma = new(this);

            Acrobatics = new();
            Arcana = new();
            Athletics = new();
            Crafting = new();
            Deception = new();
            Diplomacy = new();
            Intimidation = new();
            Medicine = new();
            Nature = new();
            Occultism = new();
            Performance = new();
            Religion = new();
            Society = new();
            Stealth = new();
            Survival = new();
            Thievery = new();
        }

        #endregion

        #region Properties
        public User Owner { get; set; } = default!;

        public string Name { get; set; } = default!;

        public Ability Strength { get; set; }
        public Ability Dexterity { get; set; }
        public Ability Constitution { get; set; }
        public Ability Intelligence { get; set; }
        public Ability Wisdom { get; set; }
        public Ability Charisma { get; set; }

        public Skill Acrobatics { get; set; }
        public Skill Arcana { get; set; }
        public Skill Athletics { get; set; }
        public Skill Crafting { get; set; }
        public Skill Deception { get; set; }
        public Skill Diplomacy { get; set; }
        public Skill Intimidation { get; set; }
        public Skill Medicine { get; set; }
        public Skill Nature { get; set; }
        public Skill Occultism { get; set; }
        public Skill Performance { get; set; }
        public Skill Religion { get; set; }
        public Skill Society { get; set; }
        public Skill Stealth { get; set; }
        public Skill Survival { get; set; }
        public Skill Thievery { get; set; }

        #endregion
    }
}
