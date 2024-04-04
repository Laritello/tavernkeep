import type { Ability, Health, Lore, Skill } from '@/contracts/character';
import type { AbilityType, SkillType } from '@/contracts/enums';

export interface Character {
    id: string;
    name: string;
    ownerId: string;

    health: Health;
    abilities: Record<AbilityType, Ability>;
    skills: Record<SkillType, Skill>;

    lores: Lore[];
}
