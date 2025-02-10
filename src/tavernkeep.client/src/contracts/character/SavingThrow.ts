import type { Proficiency, SkillDataType, SavingThrowType } from '@/contracts/enums';

export interface SavingThrow {
    type: SkillDataType;
    name: SavingThrowType;
    proficiency: Proficiency;
    bonus: number;
}
