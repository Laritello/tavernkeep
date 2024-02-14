import { Proficiency } from "../enums/Proficiency";
import type { SkillType } from "../enums/SkillType";

export class Skill {
    type: SkillType
    proficiency: Proficiency

    constructor(type: SkillType) {
        this.type = type;
        this.proficiency = Proficiency.Untrained;
    }
}