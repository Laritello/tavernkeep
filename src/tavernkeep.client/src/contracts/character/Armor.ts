import type { ArmorType, Proficiency } from '../enums';

export interface Armor {
    class: number;
    proficiencies: Record<ArmorType, Proficiency>;
    equipped: EquippedArmor;
}

export interface EquippedArmor {
    type: ArmorType;
    bonus: number;
    hasDexterityCap: boolean;
    dexterityCap: number;
}
