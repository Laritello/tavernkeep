import type { Proficiency, SkillType } from '@/contracts/enums';
import type { BaseSkill } from './BaseSkill';

export interface Skill extends BaseSkill{
    type: SkillType;
    name: string;
    proficiency: Proficiency;
    bonus: number;
    
}
