import type { Proficiency } from '@/contracts/enums';

export interface Skill {
    name: string;
    proficiency: Proficiency;
    bonus: number;
}
