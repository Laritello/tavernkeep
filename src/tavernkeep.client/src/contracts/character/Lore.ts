import { Proficiency } from '@/contracts/enums';

export interface Lore {
    topic: string;
    proficiency: Proficiency;
    bonus: number;
}
