import type { Ability, Armor, Perception, SavingThrow, Skill, Speed } from '@/contracts/character';
import {
    AbilityType,
    AncestryType,
    ArmorType,
    ClassType,
    Proficiency,
    SavingThrowType,
    SkillType,
    SpeedType,
} from '@/contracts/enums';

// region Types
type AbilityBoostType = keyof typeof AbilityType | 'any';
type AbilityFlawType = keyof typeof AbilityType;
type BaseSpeed = { type: SpeedType; value: number };

type AbilitiesTemplate = Omit<Ability, 'modifier'>[];
type SavingThrowsTemplate = Omit<SavingThrow, 'type' | 'bonus'>[];
type PerceptionTemplate = Omit<Perception, 'type' | 'name' | 'bonus'>;
type SkillsTemplate = Omit<Skill, 'type' | 'pinned' | 'bonus'>[];
type SpeedsTemplate = Omit<Speed, 'value' | 'active'>[];
type DefencesTemplate = Omit<Armor, 'class' | 'equipped'>;
//endregion

//region Interfaces
interface CharacterTemplate {
    name: string;
    level: number;
    ancestry: AncestryTemplate;
    class: ClassTemplate;
    abilities: AbilitiesTemplate;
    speeds: SpeedsTemplate;
    savingThrows: SavingThrowsTemplate;
    perception: PerceptionTemplate;
    skills: SkillsTemplate;
    armor: DefencesTemplate;
}

interface AncestryTemplate {
    type: AncestryType;
    baseHealth: number;
}

interface ClassTemplate {
    type: ClassType;
    healthPerLevel: number;
}
//endregion

//region Configuration
type AncestryConfig = Omit<AncestryTemplate, 'type'> & {
    speed: BaseSpeed;
    abilityBoost: AbilityBoostType | AbilityBoostType[];
    abilityFlaw?: AbilityFlawType;
};

type ClassConfig = Omit<ClassTemplate, 'type'> & {
    keyAbility: AbilityType | AbilityType[];
    perception: Proficiency;
    savingThrows: Record<SavingThrowType, Proficiency>;
    additionalSkillsCount: number;
    trainedSkills: SkillType[];
    armorProficiencies: Record<ArmorType, Proficiency>;
};

const ANCESTRY_CONFIG: Record<AncestryType, AncestryConfig> = {
    [AncestryType.Catfolk]: {
        baseHealth: 8,
        speed: { type: SpeedType.Walk, value: 25 },
        abilityBoost: ['Dexterity', 'Charisma', 'any'],
        abilityFlaw: 'Wisdom',
    },
    [AncestryType.Dwarf]: {
        baseHealth: 10,
        speed: { type: SpeedType.Walk, value: 20 },
        abilityBoost: ['Constitution', 'Wisdom', 'any'],
        abilityFlaw: 'Charisma',
    },
    [AncestryType.Elf]: {
        baseHealth: 6,
        speed: { type: SpeedType.Walk, value: 30 },
        abilityBoost: ['Dexterity', 'Intelligence', 'any'],
        abilityFlaw: 'Constitution',
    },
    [AncestryType.Gnome]: {
        baseHealth: 8,
        speed: { type: SpeedType.Walk, value: 25 },
        abilityBoost: ['Constitution', 'Charisma', 'any'],
        abilityFlaw: 'Strength',
    },
    [AncestryType.Goblin]: {
        baseHealth: 6,
        speed: { type: SpeedType.Walk, value: 25 },
        abilityBoost: ['Dexterity', 'Charisma', 'any'],
        abilityFlaw: 'Wisdom',
    },
    [AncestryType.Halfling]: {
        baseHealth: 6,
        speed: { type: SpeedType.Walk, value: 25 },
        abilityBoost: ['Dexterity', 'Wisdom', 'any'],
        abilityFlaw: 'Strength',
    },
    [AncestryType.Human]: {
        baseHealth: 8,
        speed: { type: SpeedType.Walk, value: 25 },
        abilityBoost: ['any', 'any'],
    },
    [AncestryType.Kobold]: {
        baseHealth: 6,
        speed: { type: SpeedType.Walk, value: 25 },
        abilityBoost: ['Dexterity', 'Charisma', 'any'],
        abilityFlaw: 'Constitution',
    },
    [AncestryType.Leshy]: {
        baseHealth: 8,
        speed: { type: SpeedType.Walk, value: 25 },
        abilityBoost: ['Constitution', 'Wisdom', 'any'],
        abilityFlaw: 'Intelligence',
    },
    [AncestryType.Orc]: {
        baseHealth: 10,
        speed: { type: SpeedType.Walk, value: 25 },
        abilityBoost: ['any', 'any'],
    },
    [AncestryType.Ratfolk]: {
        baseHealth: 6,
        speed: { type: SpeedType.Walk, value: 25 },
        abilityBoost: ['Dexterity', 'Intelligence', 'any'],
        abilityFlaw: 'Strength',
    },
    [AncestryType.Tengu]: {
        baseHealth: 6,
        speed: { type: SpeedType.Walk, value: 25 },
        abilityBoost: ['Dexterity', 'any'],
    },
};

const CLASS_CONFIG: Record<ClassType, ClassConfig> = {
    [ClassType.Alchemist]: {
        healthPerLevel: 8,
        keyAbility: AbilityType.Intelligence,
        perception: Proficiency.Trained,
        savingThrows: {
            Fortitude: Proficiency.Expert,
            Reflex: Proficiency.Expert,
            Will: Proficiency.Trained,
        },

        trainedSkills: [SkillType.Crafting],
        additionalSkillsCount: 3,

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Trained,
            Medium: Proficiency.Trained,
            Heavy: Proficiency.Untrained,
        },
    },
    [ClassType.Barbarian]: {
        healthPerLevel: 12,
        keyAbility: AbilityType.Strength,
        perception: Proficiency.Expert,
        savingThrows: {
            Fortitude: Proficiency.Expert,
            Reflex: Proficiency.Trained,
            Will: Proficiency.Expert,
        },

        trainedSkills: [SkillType.Athletics],
        additionalSkillsCount: 3,

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Trained,
            Medium: Proficiency.Trained,
            Heavy: Proficiency.Untrained,
        },
    },
    [ClassType.Bard]: {
        healthPerLevel: 8,
        keyAbility: AbilityType.Charisma,
        perception: Proficiency.Expert,
        savingThrows: {
            Fortitude: Proficiency.Trained,
            Reflex: Proficiency.Trained,
            Will: Proficiency.Expert,
        },

        trainedSkills: [SkillType.Occultism, SkillType.Performance],
        additionalSkillsCount: 4,

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Trained,
            Medium: Proficiency.Untrained,
            Heavy: Proficiency.Untrained,
        },
    },
    [ClassType.Champion]: {
        healthPerLevel: 10,
        keyAbility: [AbilityType.Strength, AbilityType.Dexterity],
        perception: Proficiency.Trained,
        savingThrows: {
            Fortitude: Proficiency.Expert,
            Reflex: Proficiency.Trained,
            Will: Proficiency.Expert,
        },

        trainedSkills: [SkillType.Religion],
        additionalSkillsCount: 3, // 2 + 1 from deity

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Trained,
            Medium: Proficiency.Trained,
            Heavy: Proficiency.Trained,
        },
    },
    [ClassType.Cleric]: {
        healthPerLevel: 8,
        keyAbility: AbilityType.Wisdom,
        perception: Proficiency.Trained,
        savingThrows: {
            Fortitude: Proficiency.Trained,
            Reflex: Proficiency.Trained,
            Will: Proficiency.Expert,
        },

        trainedSkills: [SkillType.Religion],
        additionalSkillsCount: 3, // 2 + 1 from deity

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Untrained,
            Medium: Proficiency.Untrained,
            Heavy: Proficiency.Untrained,
        },
    },
    [ClassType.Druid]: {
        healthPerLevel: 8,
        keyAbility: AbilityType.Wisdom,
        perception: Proficiency.Trained,
        savingThrows: {
            Fortitude: Proficiency.Trained,
            Reflex: Proficiency.Trained,
            Will: Proficiency.Expert,
        },

        trainedSkills: [SkillType.Nature],
        additionalSkillsCount: 3, // 2 + 1 from order

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Trained,
            Medium: Proficiency.Trained,
            Heavy: Proficiency.Untrained,
        },
    },
    [ClassType.Fighter]: {
        healthPerLevel: 10,
        keyAbility: [AbilityType.Strength, AbilityType.Dexterity],
        perception: Proficiency.Expert,
        savingThrows: {
            Fortitude: Proficiency.Expert,
            Reflex: Proficiency.Expert,
            Will: Proficiency.Trained,
        },

        trainedSkills: [SkillType.Athletics], // or Acrobatics
        additionalSkillsCount: 3,

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Trained,
            Medium: Proficiency.Trained,
            Heavy: Proficiency.Trained,
        },
    },
    [ClassType.Investigator]: {
        healthPerLevel: 8,
        keyAbility: AbilityType.Intelligence,
        perception: Proficiency.Expert,
        savingThrows: {
            Fortitude: Proficiency.Trained,
            Reflex: Proficiency.Expert,
            Will: Proficiency.Expert,
        },

        trainedSkills: [SkillType.Society],
        additionalSkillsCount: 5, // 4 + 1 methodology

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Trained,
            Medium: Proficiency.Untrained,
            Heavy: Proficiency.Untrained,
        },
    },
    [ClassType.Monk]: {
        healthPerLevel: 10,
        keyAbility: [AbilityType.Strength, AbilityType.Dexterity],
        perception: Proficiency.Trained,
        savingThrows: {
            Fortitude: Proficiency.Expert,
            Reflex: Proficiency.Expert,
            Will: Proficiency.Expert,
        },

        trainedSkills: [],
        additionalSkillsCount: 4,

        armorProficiencies: {
            Unarmored: Proficiency.Expert,
            Light: Proficiency.Untrained,
            Medium: Proficiency.Untrained,
            Heavy: Proficiency.Untrained,
        },
    },
    [ClassType.Oracle]: {
        healthPerLevel: 8,
        keyAbility: AbilityType.Charisma,
        perception: Proficiency.Trained,
        savingThrows: {
            Fortitude: Proficiency.Trained,
            Reflex: Proficiency.Trained,
            Will: Proficiency.Expert,
        },

        trainedSkills: [SkillType.Religion],
        additionalSkillsCount: 4, // 3 + 1 from class

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Trained,
            Medium: Proficiency.Untrained,
            Heavy: Proficiency.Untrained,
        },
    },
    [ClassType.Ranger]: {
        healthPerLevel: 10,
        keyAbility: [AbilityType.Strength, AbilityType.Dexterity],
        perception: Proficiency.Expert,
        savingThrows: {
            Fortitude: Proficiency.Expert,
            Reflex: Proficiency.Expert,
            Will: Proficiency.Trained,
        },

        trainedSkills: [SkillType.Nature, SkillType.Survival],
        additionalSkillsCount: 4,

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Trained,
            Medium: Proficiency.Trained,
            Heavy: Proficiency.Untrained,
        },
    },
    [ClassType.Rogue]: {
        healthPerLevel: 8,
        keyAbility: [AbilityType.Strength, AbilityType.Dexterity, AbilityType.Intelligence, AbilityType.Wisdom],
        perception: Proficiency.Expert,
        savingThrows: {
            Fortitude: Proficiency.Trained,
            Reflex: Proficiency.Expert,
            Will: Proficiency.Expert,
        },

        trainedSkills: [SkillType.Stealth],
        additionalSkillsCount: 8, // 7 + 1 from class

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Trained,
            Medium: Proficiency.Untrained,
            Heavy: Proficiency.Untrained,
        },
    },
    [ClassType.Sorcerer]: {
        healthPerLevel: 6,
        keyAbility: AbilityType.Charisma,
        perception: Proficiency.Expert,
        savingThrows: {
            Fortitude: Proficiency.Trained,
            Reflex: Proficiency.Trained,
            Will: Proficiency.Expert,
        },

        trainedSkills: [],
        additionalSkillsCount: 3, // 2 + 1 from class

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Untrained,
            Medium: Proficiency.Untrained,
            Heavy: Proficiency.Untrained,
        },
    },
    [ClassType.Swashbuckler]: {
        healthPerLevel: 10,
        keyAbility: AbilityType.Dexterity,
        perception: Proficiency.Expert,
        savingThrows: {
            Fortitude: Proficiency.Trained,
            Reflex: Proficiency.Expert,
            Will: Proficiency.Expert,
        },

        trainedSkills: [SkillType.Acrobatics],
        additionalSkillsCount: 5, // 4 + 1 from class

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Trained,
            Medium: Proficiency.Untrained,
            Heavy: Proficiency.Untrained,
        },
    },
    [ClassType.Witch]: {
        healthPerLevel: 6,
        keyAbility: AbilityType.Intelligence,
        perception: Proficiency.Trained,
        savingThrows: {
            Fortitude: Proficiency.Trained,
            Reflex: Proficiency.Trained,
            Will: Proficiency.Expert,
        },

        trainedSkills: [],
        additionalSkillsCount: 4, // 3 + 1 from class

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Untrained,
            Medium: Proficiency.Untrained,
            Heavy: Proficiency.Untrained,
        },
    },
    [ClassType.Wizard]: {
        healthPerLevel: 6,
        keyAbility: AbilityType.Intelligence,
        perception: Proficiency.Trained,
        savingThrows: {
            Fortitude: Proficiency.Trained,
            Reflex: Proficiency.Trained,
            Will: Proficiency.Expert,
        },

        trainedSkills: [SkillType.Arcana],
        additionalSkillsCount: 2,

        armorProficiencies: {
            Unarmored: Proficiency.Trained,
            Light: Proficiency.Untrained,
            Medium: Proficiency.Untrained,
            Heavy: Proficiency.Untrained,
        },
    },
};
//endregion

export class CharacterBuilder {
    private template: Partial<CharacterTemplate> = {
        level: 1,
        abilities: this.createBaseAbilities(),
        skills: this.createBaseSkills(),
    };

    constructor(name: string) {
        this.template.name = name;
    }

    setLevel(level: number): this {
        this.template.level = level;
        return this;
    }

    setAncestry(ancestry: AncestryType): this {
        const config = ANCESTRY_CONFIG[ancestry];
        this.template.ancestry = {
            type: ancestry,
            ...config,
        };
        this.template.speeds = [{ type: SpeedType.Walk, base: config.speed.value }];
        return this;
    }

    setClass(cls: ClassType): this {
        const config = CLASS_CONFIG[cls];
        this.template.class = { type: cls, ...config };

        this.template.savingThrows = this.createSavingThrows(config);
        this.template.perception = { proficiency: config.perception };
        this.template.armor = { proficiencies: config.armorProficiencies };
        this.addTrainedSkills(config.trainedSkills);

        return this;
    }

    build(): CharacterTemplate {
        return this.template as CharacterTemplate;
    }

    private createBaseAbilities(): AbilitiesTemplate {
        return ['Strength', 'Dexterity', 'Constitution', 'Intelligence', 'Wisdom', 'Charisma'].map((type) => ({
            name: type as AbilityType,
            score: 10,
        }));
    }

    private createBaseSkills(): SkillsTemplate {
        return Object.values(SkillType).map((type) => ({
            name: type,
            proficiency: Proficiency.Untrained,
        }));
    }

    private createSavingThrows(config: ClassConfig): SavingThrowsTemplate {
        return [
            { name: SavingThrowType.Fortitude, proficiency: config.savingThrows.Fortitude },
            { name: SavingThrowType.Reflex, proficiency: config.savingThrows.Reflex },
            { name: SavingThrowType.Will, proficiency: config.savingThrows.Will },
        ];
    }

    private addTrainedSkills(skills: SkillType[]): void {
        skills.forEach((skillType) => {
            const skill = this.template.skills?.find((s) => s.name === skillType);
            if (skill) skill.proficiency = Proficiency.Trained;
        });
    }
}
