import type { SpeedType } from '../enums/SpeedType';

export interface Speed {
    type: SpeedType;
    active: boolean;
    base: number;
    value: number;
}
