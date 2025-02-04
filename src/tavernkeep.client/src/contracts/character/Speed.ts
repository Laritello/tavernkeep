import type { SpeedType } from '@/contracts/enums';

export interface Speed {
    type: SpeedType;
    active: boolean;
    base: number;
    value: number;
}
