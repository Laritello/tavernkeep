import type { Proficiency, SkillType } from '@/contracts/enums';

export interface Perception {
    type: SkillType;
    name: string;
    proficiency: Proficiency;
    bonus: number;
}
