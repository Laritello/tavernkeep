import type { Ability, Health, Lore, Perception, Skill } from '@/contracts/character';
import type { AbilityType, SavingThrowType, SkillType } from '@/contracts/enums';
import type { Condition } from './Condition';
import type { SavingThrow } from '@/contracts/character/SavingThrow';
import type { ArmorClass } from '../contracts/character/ArmorClass';

export interface Character {
    id: string;
    name: string;
    ownerId: string;

    health: Health;
    perception: Perception;
    abilities: Record<AbilityType, Ability>;
    skills: Record<SkillType, Skill>;
    savingThrows: Record<SavingThrowType, SavingThrow>;
    armor: ArmorClass;

    lores: Lore[];
    conditions: Condition[];
}
