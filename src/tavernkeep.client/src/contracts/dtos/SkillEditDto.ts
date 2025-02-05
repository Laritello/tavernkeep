import type { Proficiency } from "../enums";

export interface SkillEditDto {
    proficiency?: Proficiency;
    pinned?: boolean;
}
