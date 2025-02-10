import { AbilityType } from '@/contracts/enums';

export interface Ability {
    name: AbilityType;
    score: number;
    modifier: number;
}
