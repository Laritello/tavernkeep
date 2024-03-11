import { Ability } from '@/contracts/character/Ability';
import { Health } from '@/contracts/character/Health';
import type { Lore } from '@/contracts/character/Lore';
import { Skill } from '@/contracts/character/Skill';
import { AbilityType } from '@/contracts/enums/AbilityType';
import { SkillType } from '@/contracts/enums/SkillType';
import { type User } from '@/entities/User';

export interface Character {
    id: string;
    name: string;
    owner: User;

    health: Health;
    abilities: Record<AbilityType, Ability>;
    skills: Record<SkillType, Skill>;

    lores: Lore[];
}
