import type { Proficiency } from "../enums/Proficiency"
import type { SkillType } from "../enums/SkillType"

export class SkillEditedNotification {
    characterId: string
    type: SkillType
    proficiency: Proficiency

    constructor(characterId: string, type: SkillType, proficiency: Proficiency) {
        this.characterId = characterId;
        this.type = type;
        this.proficiency = proficiency;
    }
}