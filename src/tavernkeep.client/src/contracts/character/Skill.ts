import type { Proficiency, SkillType } from '@/contracts/enums';

export interface Skill {
    type: SkillType;
    name: string;
    proficiency: Proficiency;
    pinned: boolean;
    bonus: number;
}
