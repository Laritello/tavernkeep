import type { AbilityType } from '@/contracts/enums';

export interface AbilityEditedNotification {
    characterId: string;
    type: AbilityType;
    score: number;
}
