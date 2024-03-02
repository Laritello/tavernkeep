import { Ability } from '@/contracts/character/Ability';
import { Health } from '@/contracts/character/Health';
import { Skill } from '@/contracts/character/Skill';
import { AbilityType } from '@/contracts/enums/AbilityType';
import { SkillType } from '@/contracts/enums/SkillType';
import { User } from '@/entities/User';

import { Type } from 'class-transformer';

export class Character {
    id: string;
    name: string;
    owner: User;

    health: Health;

    @Type(() => Map<AbilityType, Ability>)
    abilities: Map<AbilityType, Ability>;

    @Type(() => Map<SkillType, Skill>)
    skills: Map<SkillType, Skill>;

    constructor(id: string, name: string, owner: User) {
        this.id = id;
        this.name = name;
        this.owner = owner;

        this.health = new Health(0, 0, 0);

        this.abilities = new Map([
            [AbilityType.Strength, new Ability(AbilityType.Strength)],
            [AbilityType.Dexterity, new Ability(AbilityType.Dexterity)],
            [AbilityType.Constitution, new Ability(AbilityType.Constitution)],
            [AbilityType.Intelligence, new Ability(AbilityType.Intelligence)],
            [AbilityType.Wisdom, new Ability(AbilityType.Wisdom)],
            [AbilityType.Charisma, new Ability(AbilityType.Charisma)],
        ]);

        this.skills = new Map([
            [SkillType.Acrobatics, new Skill(SkillType.Acrobatics)],
            [SkillType.Arcana, new Skill(SkillType.Arcana)],
            [SkillType.Athletics, new Skill(SkillType.Athletics)],
            [SkillType.Crafting, new Skill(SkillType.Crafting)],
            [SkillType.Deception, new Skill(SkillType.Deception)],
            [SkillType.Diplomacy, new Skill(SkillType.Diplomacy)],
            [SkillType.Intimidation, new Skill(SkillType.Intimidation)],
            [SkillType.Medicine, new Skill(SkillType.Medicine)],
            [SkillType.Nature, new Skill(SkillType.Nature)],
            [SkillType.Occultism, new Skill(SkillType.Occultism)],
            [SkillType.Performance, new Skill(SkillType.Performance)],
            [SkillType.Religion, new Skill(SkillType.Religion)],
            [SkillType.Society, new Skill(SkillType.Society)],
            [SkillType.Stealth, new Skill(SkillType.Stealth)],
            [SkillType.Survival, new Skill(SkillType.Survival)],
            [SkillType.Thievery, new Skill(SkillType.Thievery)],
        ]);
    }
}
