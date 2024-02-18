using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Character;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities
{
    [Table("Characters")]
    public class Character : Entity
    {
        #region Constructors

        public Character()
        {
            Health = new();

            Strength = new(this, AbilityType.Strength);
            Dexterity = new(this, AbilityType.Dexterity);
            Constitution = new(this, AbilityType.Constitution);
            Intelligence = new(this, AbilityType.Intelligence);
            Wisdom = new(this, AbilityType.Wisdom);
            Charisma = new(this, AbilityType.Charisma);

            Acrobatics = new(this, SkillType.Acrobatics);
            Arcana = new(this, SkillType.Arcana);
            Athletics = new(this, SkillType.Athletics);
            Crafting = new(this, SkillType.Crafting);
            Deception = new(this, SkillType.Deception);
            Diplomacy = new(this, SkillType.Diplomacy);
            Intimidation = new(this, SkillType.Intimidation);
            Medicine = new(this, SkillType.Medicine);
            Nature = new(this, SkillType.Nature);
            Occultism = new(this, SkillType.Occultism);
            Performance = new(this, SkillType.Performance);
            Religion = new(this, SkillType.Religion);
            Society = new(this, SkillType.Society);
            Stealth = new(this, SkillType.Stealth);
            Survival = new(this, SkillType.Survival);
            Thievery = new(this, SkillType.Thievery);
        }

        #endregion

        #region Properties
        public User Owner { get; set; } = default!;

        public string Name { get; set; } = default!;

        public Health Health { get; set; }

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

        #region Methods

        public Ability GetAbility(AbilityType type)
        {
            return type switch
            {
                AbilityType.Strength => Strength,
                AbilityType.Dexterity => Dexterity,
                AbilityType.Constitution => Constitution,
                AbilityType.Intelligence => Intelligence,
                AbilityType.Wisdom => Wisdom,
                AbilityType.Charisma => Charisma,
                _ => throw new NotImplementedException(),
            };
        }

        public Skill GetSkill(SkillType type)
        {
            return type switch
            {
                SkillType.Acrobatics => Acrobatics,
                SkillType.Deception => Deception,
                SkillType.Arcana => Arcana,
                SkillType.Athletics => Athletics,
                SkillType.Crafting => Crafting,
                SkillType.Diplomacy => Diplomacy,
                SkillType.Intimidation => Intimidation,
                SkillType.Medicine => Medicine,
                SkillType.Nature => Nature,
                SkillType.Occultism => Occultism,
                SkillType.Performance => Performance,
                SkillType.Religion => Religion,
                SkillType.Society => Society,
                SkillType.Stealth => Stealth,
                SkillType.Survival => Survival,
                SkillType.Thievery => Thievery,
                _ => throw new NotImplementedException(),
            };
        }

        #endregion
    }
}
