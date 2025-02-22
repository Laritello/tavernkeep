import type { UnitSize } from '@/contracts/creatures/UnitSize.ts';
import type { Rarity } from '@/contracts/enums/Rarity.ts';

export interface CreatureShort {
    id: string;
    name: string;
    level: number;
    size: UnitSize;
    rarity: Rarity;
    traits: string[];
}
