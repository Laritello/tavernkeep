import type { Proficiency, SkillDataType, SkillType } from '@/contracts/enums';

export interface Skill {
    type: SkillDataType;
    name: SkillType;
    proficiency: Proficiency;
    pinned: boolean;
    bonus: number;
}
