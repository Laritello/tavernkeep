import { Proficiency, SkillType } from '@/contracts/enums';

export interface BaseSkill {
    type: SkillType;
    name: string;
    proficiency: Proficiency;
}