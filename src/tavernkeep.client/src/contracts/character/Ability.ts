import type { AbilityType } from '@/contracts/enums';

export interface Ability {
    type: AbilityType;
    score: number;
    modifier: number;
}
