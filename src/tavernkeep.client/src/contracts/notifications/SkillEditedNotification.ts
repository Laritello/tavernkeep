import type { Proficiency, SkillType } from '@/contracts/enums';

export interface SkillEditedNotification {
    characterId: string;
    type: SkillType;
    proficiency: Proficiency;
}
