import type { Health } from '@/contracts/character';
import { ParticipantType, SavingThrowType } from '@/contracts/enums';

export interface Participant {
    type: ParticipantType;
    id: string;
    name: string;
    isActiveTurn: boolean;
    entityId: string;
    initiative: number | null;
    armorClass: number;
    perception: number;
    health: Health;
    savingThrows: Record<SavingThrowType, number>;
}
