import type { Proficiency, SkillDataType } from '@/contracts/enums';

export interface Perception {
    type: SkillDataType.Perception;
    name: 'Perception';
    proficiency: Proficiency;
    bonus: number;
}
