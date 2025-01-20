import type { Proficiency, SkillType } from '@/contracts/enums';

export interface Skill {
    name: string;
    proficiency: Proficiency;
    bonus: number;
    type: SkillType;
}
