import type { Character } from "@/entities/Character";
import type { AbilityType } from "../enums/AbilityType";

export class Ability {
    owner: Character
    type: AbilityType
    score: number

    constructor(owner: Character, type: AbilityType) {
        this.owner = owner;
        this.type = type;
        this.score = 0;
    }
}