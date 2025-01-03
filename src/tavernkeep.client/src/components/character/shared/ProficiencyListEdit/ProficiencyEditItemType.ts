import { Proficiency } from '@/contracts/enums';

export type ProficiencyEditItemType = {
    name: string,
    proficiency: Proficiency,
    userBonus: number
}