import type { Ability, Health, Lore, Skill } from '@/contracts/character';
import type { AbilityType, SkillType } from '@/contracts/enums';
import type { User } from './User';

export interface Character {
    id: string;
    name: string;
    owner: User;

    health: Health;
    abilities: Record<AbilityType, Ability>;
    skills: Record<SkillType, Skill>;

    lores: Lore[];
}
