import type { Proficiency } from '@/contracts/enums';

export interface SavingThrow {
    name: string;
    proficiency: Proficiency;
    bonus: number;
}
