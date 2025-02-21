import type { Rarity } from '@/contracts/Rarity.ts';
import type { UnitSize } from '@/contracts/creatures/UnitSize.ts';

export interface CreatureShort {
    id: string;
    name: string;
    level: number;
    size: UnitSize;
    rarity: Rarity;
    traits: string[];
}
