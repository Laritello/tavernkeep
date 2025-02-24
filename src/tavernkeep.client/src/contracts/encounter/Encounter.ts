import { EncounterStateType } from '@/contracts/encounter/EncounterStateType.ts';
import type { Participant } from '@/contracts/encounter/Participant.ts';

export interface Encounter {
    id: string;
    name: string;
    status: EncounterStateType;
    roundNumber: number;
    currentTurnIndex: number;
    participants: Participant[];
    createdAt: number;
}
