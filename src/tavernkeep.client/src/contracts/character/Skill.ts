import type { Character } from "@/entities/Character";
import { Proficiency } from "../enums/Proficiency";
import type { SkillType } from "../enums/SkillType";

export class Skill {
    owner: Character
    type: SkillType
    proficiency: Proficiency

    constructor(owner: Character, type: SkillType) {
        this.owner = owner;
        this.type = type;
        this.proficiency = Proficiency.Untrained;
    }
}