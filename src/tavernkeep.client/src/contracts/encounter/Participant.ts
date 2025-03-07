import type { Health } from '@/contracts/character';
import type { ConditionShortDto } from '@/contracts/conditions/ConditionShortDto.ts';
import { ParticipantType, SavingThrowType } from '@/contracts/enums';

export interface Participant {
    type: ParticipantType;
    id: string;
    name: string;
    entityId: string;
    initiative: number | null;
    armorClass: number;
    perception: number;
    health: Health;
    savingThrows: Record<SavingThrowType, number>;
    conditions: ConditionShortDto[];
    statblock: string; // TODO: Move to a separate class
}
