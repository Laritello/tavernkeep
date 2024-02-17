import type { AbilityType } from '../enums/AbilityType';

export class AbilityEditedNotification {
    characterId: string;
    type: AbilityType;
    score: number;

    constructor(characterId: string, type: AbilityType, score: number) {
        this.characterId = characterId;
        this.type = type;
        this.score = score;
    }
}
