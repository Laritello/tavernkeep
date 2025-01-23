import type { Proficiency } from '@/contracts/enums';
import type { BaseSkill } from './BaseSkill';

export interface Perception extends BaseSkill{
    proficiency: Proficiency;
    bonus: number;
}
