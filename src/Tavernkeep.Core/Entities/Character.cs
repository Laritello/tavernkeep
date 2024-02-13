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
            
        }

        #endregion

        #region Properties

        public User Owner { get; set; } = default!;

        public Ability Strength { get; set; } = new();
        public Ability Dexterity { get; set; } = new();
        public Ability Constitution { get; set; } = new();
        public Ability Intelligence { get; set; } = new();
        public Ability Wisdom { get; set; } = new();
        public Ability Charisma { get; set; } = new();

        public Skill Acrobatics { get; set; } = new();
        public Skill Arcana { get; set; } = new();
        public Skill Athletics { get; set; } = new();
        public Skill Crafting { get; set; } = new();
        public Skill Deception { get; set; } = new();
        public Skill Diplomacy { get; set; } = new();
        public Skill Intimidation { get; set; } = new();
        public Skill Medicine { get; set; } = new();
        public Skill Nature { get; set; } = new();
        public Skill Occultism { get; set; } = new();
        public Skill Performance { get; set; } = new();
        public Skill Religion { get; set; } = new();
        public Skill Society { get; set; } = new();
        public Skill Stealth { get; set; } = new();
        public Skill Survival { get; set; } = new();
        public Skill Thievery { get; set; } = new();

        #endregion
    }
}
