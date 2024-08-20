import type { AbilityType, Proficiency, SavingThrowType } from '@/contracts/enums';

export interface SavingThrow {
    baseAbility: AbilityType;
    type: SavingThrowType;
    proficiency: Proficiency;
    bonus: number;
}
