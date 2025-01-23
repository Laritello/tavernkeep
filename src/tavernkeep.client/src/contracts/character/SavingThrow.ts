import type { Proficiency } from '@/contracts/enums';
import type { BaseSkill } from './BaseSkill';

export interface SavingThrow extends BaseSkill {
    name: string;
    proficiency: Proficiency;
    bonus: number;
}
