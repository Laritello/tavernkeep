import type { Ability, Health, Lore, Perception, Skill, Speed } from '@/contracts/character';
import type { SpeedType } from '@/contracts/enums';
import type { Condition } from './Condition';
import type { SavingThrow } from '@/contracts/character/SavingThrow';
import type { Armor } from '../contracts/character/Armor';

export interface Character {
    id: string;
    name: string;
    class: string;
    ancestry: string;
    ownerId: string;

    heroPoints: number;

    level: number;

    health: Health;
    perception: Perception;
    abilities: Record<string, Ability>;
    skills: Record<string, Skill>;
    savingThrows: Record<string, SavingThrow>;
    speeds: Record<SpeedType, Speed>;
    armor: Armor;

    lores: Lore[];
    conditions: Condition[];
}
