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

        this.strength = new Ability(this, AbilityType.Strength)
        this.dexterity = new Ability(this, AbilityType.Dexterity)
        this.consitution = new Ability(this, AbilityType.Constitution)
        this.intelligence = new Ability(this, AbilityType.Intelligence)
        this.wisdom = new Ability(this, AbilityType.Wisdom)
        this.charisma = new Ability(this, AbilityType.Charisma)

        this.acrobatics = new Skill(this, SkillType.Acrobatics)
        this.arcana = new Skill(this, SkillType.Arcana)
        this.athletics = new Skill(this, SkillType.Athletics)
        this.crafting = new Skill(this, SkillType.Crafting)
        this.deception = new Skill(this, SkillType.Deception)
        this.diplomacy = new Skill(this, SkillType.Diplomacy)
        this.intimidation = new Skill(this, SkillType.Intimidation)
        this.medicine = new Skill(this, SkillType.Medicine)
        this.nature = new Skill(this, SkillType.Nature)
        this.occultism = new Skill(this, SkillType.Occultism)
        this.performance = new Skill(this, SkillType.Performance)
        this.religion = new Skill(this, SkillType.Religion)
        this.society = new Skill(this, SkillType.Society)
        this.stealth = new Skill(this, SkillType.Stealth)
        this.survival = new Skill(this, SkillType.Survival)
        this.thievery = new Skill(this, SkillType.Thievery)
    }
}