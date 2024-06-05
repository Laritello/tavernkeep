using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavernkeep.Core.Contracts.Enums
{
    public enum ModifierTarget
    {
        None = 0,

        // Skills
        Acrobatics = 1,
        Arcana = 1 << 1,
        Athletics = 1 << 2,
        Crafting = 1 << 3,
        Deception = 1 << 4,
        Diplomacy = 1 << 5,
        Intimidation = 1 << 6,
        Medicine = 1 << 7,
        Nature = 1 << 8,
        Occultism = 1 << 9,
        Performance = 1 << 10,
        Religion = 1 << 11,
        Society = 1 << 12,
        Stealth = 1 << 13,
        Survival = 1 << 14,
        Thievery = 1 << 15,

        // Custom Skill
        Lore = 1 << 16,

        // Saves
        Fortitude = 1 << 17,
        Reflex = 1 << 18,
        Will = 1 << 19,

        // Senses
        Perception = 1 << 20,

        // Defenses
        ArmorClass = 1 << 21,

        // Offenses
        MeleeAttack = 1 << 22,
        RangedAttack = 1 << 23,
        SpellAttack = 1 << 24,
        DifficultyClass = 1 << 25,

        // Speeds
        Speed = 1 << 26, // Add separate flags for flying, burrow, etc.?

        // Groups
        SavingThrows = Fortitude | Reflex | Will,
        Attacks = MeleeAttack | RangedAttack | SpellAttack,

        // Skills by Ability
        StrengthSkills = Athletics,
        DexteritySkills = Acrobatics | Stealth | Thievery,
        IntelligenceSkills = Arcana | Crafting | Occultism | Society | Lore,
        WisdomSkills = Medicine | Nature | Religion | Survival,
        CharismaSkills = Deception | Diplomacy | Intimidation | Performance,

        // Groups by Ability
        StrengthChecks = StrengthSkills | MeleeAttack,
        DexterityChecks = DexteritySkills | Reflex | ArmorClass | RangedAttack,
        ConstitutionChecks = Fortitude,
        IntelligenceChecks = IntelligenceSkills,
        WisdomChecks = WisdomSkills | Will | Perception,
        CharismaChecks = CharismaSkills,

        // All
        AllSkills = StrengthSkills | DexteritySkills | IntelligenceSkills | WisdomSkills | CharismaSkills,
        AllChecks = StrengthChecks | DexterityChecks | ConstitutionChecks | IntelligenceChecks | WisdomChecks | CharismaChecks | DifficultyClass
    }
}
