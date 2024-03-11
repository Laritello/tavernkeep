import type { Proficiency, SkillType } from '@/contracts/enums';

export interface Skill {
    type: SkillType;
    proficiency: Proficiency;
}
