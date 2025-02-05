import type { Ability, Ancestry, Class, Health, Perception, Skill, Speed } from '@/contracts/character';
import type { Armor } from '@/contracts/character/Armor';
import type { SavingThrow } from '@/contracts/character/SavingThrow';
import type { SpeedType } from '@/contracts/enums';

import type { Condition } from './Condition';

export interface Character {
    id: string;
    name: string;
    ancestry: Ancestry;
    class: Class;
    ownerId: string;

    heroPoints: number;

    level: number;

    health: Health;
    perception: Perception;
    abilities: Ability[];
    skills: Skill[];
    savingThrows: SavingThrow[];
    speeds: Record<SpeedType, Speed>;
    armor: Armor;

    conditions: Condition[];
}
