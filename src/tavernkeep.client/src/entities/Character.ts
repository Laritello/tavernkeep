import { Ability } from "@/contracts/character/Ability";
import { Skill } from "@/contracts/character/Skill";
import { AbilityType } from "@/contracts/enums/AbilityType";
import { SkillType } from "@/contracts/enums/SkillType";

export class Character {
    name: string

    strength: Ability
    dexterity: Ability
    consitution: Ability
    intelligence: Ability
    wisdom: Ability
    charisma: Ability

    acrobatics: Skill
    arcana: Skill
    athletics: Skill
    crafting: Skill
    deception: Skill
    diplomacy: Skill
    intimidation: Skill
    medicine: Skill
    nature: Skill
    occultism: Skill
    performance: Skill
    religion: Skill
    society: Skill
    stealth: Skill
    survival: Skill
    thievery: Skill

    constructor(name: string) {
        this.name = name;

        this.strength = new Ability(AbilityType.Strength)
        this.dexterity = new Ability(AbilityType.Dexterity)
        this.consitution = new Ability(AbilityType.Constitution)
        this.intelligence = new Ability(AbilityType.Intelligence)
        this.wisdom = new Ability(AbilityType.Wisdom)
        this.charisma = new Ability(AbilityType.Charisma)

        this.acrobatics = new Skill(SkillType.Acrobatics)
        this.arcana = new Skill(SkillType.Arcana)
        this.athletics = new Skill(SkillType.Athletics)
        this.crafting = new Skill(SkillType.Crafting)
        this.deception = new Skill(SkillType.Deception)
        this.diplomacy = new Skill(SkillType.Diplomacy)
        this.intimidation = new Skill(SkillType.Intimidation)
        this.medicine = new Skill(SkillType.Medicine)
        this.nature = new Skill(SkillType.Nature)
        this.occultism = new Skill(SkillType.Occultism)
        this.performance = new Skill(SkillType.Performance)
        this.religion = new Skill(SkillType.Religion)
        this.society = new Skill(SkillType.Society)
        this.stealth = new Skill(SkillType.Stealth)
        this.survival = new Skill(SkillType.Survival)
        this.thievery = new Skill(SkillType.Thievery)
    }
}