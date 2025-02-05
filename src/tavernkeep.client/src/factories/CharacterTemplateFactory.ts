import { AncestryType, ArmorType, ClassType, Proficiency, SkillType, SpeedType } from '@/contracts/enums';

export class CharacterTemplateFactory {
    public static createCharacterTemplate(
        name: string,
        level: number,
        ancestryType: AncestryType,
        classType: ClassType
    ) {
        return {
            name,
            level,
            ...CharacterTemplateFactory.createAbilitiesTemplate(),
            ...CharacterTemplateFactory.createAncestryTemplate(ancestryType),
            ...CharacterTemplateFactory.createClassTemplate(classType),
        };
    }
    public static createAncestryTemplate(ancestry: AncestryType) {
        const template = {
            ancestry: { name: ancestry as string, health: 8 },
            speeds: { [SpeedType.Walk]: { active: true, base: 25 } },
        };

        switch (ancestry) {
            case AncestryType.Dwarf:
                template.ancestry.health = 10;
                template.speeds[SpeedType.Walk].base = 20;
                break;

            case AncestryType.Elf:
                template.ancestry.health = 6;
                template.speeds[SpeedType.Walk].base = 30;
                break;

            case AncestryType.Gnome:
                template.ancestry.health = 8;
                template.speeds[SpeedType.Walk].base = 25;
                break;

            case AncestryType.Goblin:
                template.ancestry.health = 6;
                template.speeds[SpeedType.Walk].base = 25;
                break;

            case AncestryType.Halfling:
                template.ancestry.health = 6;
                template.speeds[SpeedType.Walk].base = 25;
                break;

            case AncestryType.Human:
                template.ancestry.health = 8;
                template.speeds[SpeedType.Walk].base = 25;
                break;

            case AncestryType.Leshy:
                template.ancestry.health = 8;
                template.speeds[SpeedType.Walk].base = 25;
                break;

            case AncestryType.Orc:
                template.ancestry.health = 10;
                template.speeds[SpeedType.Walk].base = 25;
                break;
        }

        return template;
    }

    public static createClassTemplate(classType: ClassType) {
        const template = {
            class: { name: classType as string, healthPerLevel: 8 },
            savingThrows: [
                { name: 'Fortitude', type: SkillType.SavingThrow, proficiency: Proficiency.Trained },
                { name: 'Reflex', type: SkillType.SavingThrow, proficiency: Proficiency.Trained },
                { name: 'Will', type: SkillType.SavingThrow, proficiency: Proficiency.Trained },
            ],
            ...CharacterTemplateFactory.createPerceptionTemplate(classType),
            ...CharacterTemplateFactory.createSkillsTemplate(classType),
            ...CharacterTemplateFactory.createArmorTemplate(classType),
        };

        switch (classType) {
            case ClassType.Bard:
                template.class.healthPerLevel = 8;
                template.savingThrows[0].proficiency = Proficiency.Trained; // Fortitude
                template.savingThrows[1].proficiency = Proficiency.Trained; // Reflex
                template.savingThrows[2].proficiency = Proficiency.Expert; // Will
                break;

            case ClassType.Cleric:
                template.class.healthPerLevel = 8;
                template.savingThrows[0].proficiency = Proficiency.Trained; // Fortitude
                template.savingThrows[1].proficiency = Proficiency.Trained; // Reflex
                template.savingThrows[2].proficiency = Proficiency.Expert; // Will
                break;

            case ClassType.Druid:
                template.class.healthPerLevel = 8;
                template.savingThrows[0].proficiency = Proficiency.Trained; // Fortitude
                template.savingThrows[1].proficiency = Proficiency.Trained; // Reflex
                template.savingThrows[2].proficiency = Proficiency.Expert; // Will
                break;

            case ClassType.Fighter:
                template.class.healthPerLevel = 10;
                template.savingThrows[0].proficiency = Proficiency.Expert; // Fortitude
                template.savingThrows[1].proficiency = Proficiency.Expert; // Reflex
                template.savingThrows[2].proficiency = Proficiency.Trained; // Will
                break;

            case ClassType.Ranger:
                template.class.healthPerLevel = 10;
                template.savingThrows[0].proficiency = Proficiency.Expert; // Fortitude
                template.savingThrows[1].proficiency = Proficiency.Expert; // Reflex
                template.savingThrows[2].proficiency = Proficiency.Trained; // Will
                break;

            case ClassType.Rogue:
                template.class.healthPerLevel = 8;
                template.savingThrows[0].proficiency = Proficiency.Trained; // Fortitude
                template.savingThrows[1].proficiency = Proficiency.Expert; // Reflex
                template.savingThrows[2].proficiency = Proficiency.Expert; // Will
                break;

            case ClassType.Witch:
                template.class.healthPerLevel = 6;
                template.savingThrows[0].proficiency = Proficiency.Trained; // Fortitude
                template.savingThrows[1].proficiency = Proficiency.Trained; // Reflex
                template.savingThrows[2].proficiency = Proficiency.Expert; // Will
                break;

            case ClassType.Wizard:
                template.class.healthPerLevel = 6;
                template.savingThrows[0].proficiency = Proficiency.Trained; // Fortitude
                template.savingThrows[1].proficiency = Proficiency.Trained; // Reflex
                template.savingThrows[2].proficiency = Proficiency.Expert; // Will
                break;
        }

        return template;
    }

    public static createAbilitiesTemplate() {
        return {
            abilities: [
                { name: 'Strength', score: 10, modifier: 0 },
                { name: 'Dexterity', score: 10, modifier: 0 },
                { name: 'Constitution', score: 10, modifier: 0 },
                { name: 'Intelligence', score: 10, modifier: 0 },
                { name: 'Wisdom', score: 10, modifier: 0 },
                { name: 'Charisma', score: 10, modifier: 0 },
            ],
        };
    }

    public static createSkillsTemplate(classType: ClassType) {
        const template = {
            skills: [
                { name: 'Acrobatics', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 0
                { name: 'Arcana', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 1
                { name: 'Athletics', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 2
                { name: 'Crafting', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 3
                { name: 'Deception', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 4
                { name: 'Diplomacy', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 5
                { name: 'Intimidation', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 6
                { name: 'Medicine', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 7
                { name: 'Nature', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 8
                { name: 'Occultism', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 9
                { name: 'Performance', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 10
                { name: 'Religion', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 11
                { name: 'Society', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 12
                { name: 'Stealth', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 13
                { name: 'Survival', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, //14
                { name: 'Thievery', type: SkillType.Basic, proficiency: Proficiency.Untrained, bonus: 0 }, // 15
            ],
        };

        switch (classType) {
            case ClassType.Bard:
                template.skills[9].proficiency = Proficiency.Trained;
                template.skills[10].proficiency = Proficiency.Trained;
                break;
            case ClassType.Cleric:
                template.skills[11].proficiency = Proficiency.Trained;
                break;

            case ClassType.Druid:
                template.skills[8].proficiency = Proficiency.Trained;
                break;

            case ClassType.Ranger:
                template.skills[8].proficiency = Proficiency.Trained;
                template.skills[14].proficiency = Proficiency.Trained;
                break;

            case ClassType.Rogue:
                template.skills[15].proficiency = Proficiency.Trained;
                break;

            case ClassType.Wizard:
                template.skills[2].proficiency = Proficiency.Trained;
                break;

            case ClassType.Witch:
            case ClassType.Fighter:
                break;
        }

        return template;
    }

    public static createArmorTemplate(classType: ClassType) {
        const template = {
            armor: {
                class: 0,
                proficiencies: {
                    [ArmorType.Unarmored]: Proficiency.Trained,
                    [ArmorType.Light]: Proficiency.Untrained,
                    [ArmorType.Medium]: Proficiency.Untrained,
                    [ArmorType.Heavy]: Proficiency.Untrained,
                },
            },
        };

        switch (classType) {
            case ClassType.Bard:
                template.armor.proficiencies.Light = Proficiency.Trained;
                break;

            case ClassType.Druid:
                template.armor.proficiencies.Light = Proficiency.Trained;
                template.armor.proficiencies.Medium = Proficiency.Trained;
                break;

            case ClassType.Fighter:
                template.armor.proficiencies.Light = Proficiency.Trained;
                template.armor.proficiencies.Medium = Proficiency.Trained;
                template.armor.proficiencies.Heavy = Proficiency.Trained;
                break;

            case ClassType.Ranger:
                template.armor.proficiencies.Light = Proficiency.Trained;
                template.armor.proficiencies.Medium = Proficiency.Trained;
                break;

            case ClassType.Rogue:
                template.armor.proficiencies.Light = Proficiency.Trained;
                break;

            case ClassType.Cleric:
            case ClassType.Witch:
            case ClassType.Wizard:
                break;
        }

        return template;
    }

    public static createPerceptionTemplate(classType: ClassType) {
        const experts = [ClassType.Bard, ClassType.Fighter, ClassType.Ranger, ClassType.Rogue];
        return {
            perception: {
                name: 'Perception',
                type: SkillType.Perception,
                proficiency: experts.includes(classType) ? Proficiency.Expert : Proficiency.Trained,
            },
        };
    }
}
