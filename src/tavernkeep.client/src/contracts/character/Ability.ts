import type { AbilityType } from '../enums/AbilityType';

export class Ability {
    type: AbilityType;
    score: number;

    constructor(type: AbilityType) {
        this.type = type;
        this.score = 0;
    }
}
