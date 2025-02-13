import type { Participant } from '@/contracts/encounter/Participant.ts';

export interface Encounter {
    id: string;
    name: string;
    participants: Participant[];
    // description: string;
    // round: number;
    // currentTurnIndex: number;
    // notes: string;
}
