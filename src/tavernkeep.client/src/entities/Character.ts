import type { Ability, Health, Lore, Perception, Skill } from '@/contracts/character';
import type { AbilityType, SavingThrowType, SkillType } from '@/contracts/enums';
import type { Condition } from './Condition';
import type { SavingThrow } from '@/contracts/character/SavingThrow';
import type { Armor } from '../contracts/character/Armor';

export interface Character {
    id: string;
    name: string;
    class: string;
    ancestry: string;
    ownerId: string;

    level: number;

    health: Health;
    perception: Perception;
    abilities: Record<AbilityType, Ability>;
    skills: Record<SkillType, Skill>;
    savingThrows: Record<SavingThrowType, SavingThrow>;
    armor: Armor;

    lores: Lore[];
    conditions: Condition[];
}
