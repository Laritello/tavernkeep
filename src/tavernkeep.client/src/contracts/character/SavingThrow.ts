import type { Proficiency, SkillType } from '@/contracts/enums';

export interface SavingThrow {
    type: SkillType;
    name: string;
    proficiency: Proficiency;
    bonus: number;
}
