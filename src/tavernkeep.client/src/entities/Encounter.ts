// models/Encounter.ts
import { v4 as uuidv4 } from 'uuid';

import type { Character } from '@/entities/Character.ts';
import type { DTO } from '@/types';

export class Encounter {
    public readonly id: string;
    public name: string;
    public description: string;
    public round: number;
    public participants: Participant[];
    public currentTurnIndex: number;
    public history: EncounterState[];
    public notes: string;
    public difficulty?: string;
    public experience?: number;

    constructor(config?: Partial<EncounterConfig>) {
        this.id = uuidv4();
        this.name = config?.name || `Encounter ${this.id.slice(0, 6)}`;
        this.description = config?.description || '';
        this.round = 1;
        this.participants = [];
        this.currentTurnIndex = -1;
        this.history = [];
        this.notes = '';
    }

    public serialize(): SerializedEncounter {
        return {
            id: this.id,
            name: this.name,
            description: this.description,
            round: this.round,
            participants: this.participants.map((p) => ({ ...p })),
            currentTurnIndex: this.currentTurnIndex,
            history: this.history.map((h) => ({ ...h })),
            notes: this.notes,
            difficulty: this.difficulty,
            experience: this.experience,
        };
    }

    public static deserialize(data: SerializedEncounter): Encounter {
        const encounter = new Encounter();
        Object.assign(encounter, data);
        return encounter;
    }

    public rollInitiative(participant: Participant) {
        // TODO: Implement initiative rolls
        this.saveHistoryState();
    }

    public nextTurn() {
        this.saveHistoryState();

        if (this.currentTurnIndex >= this.participants.length - 1) {
            this.round++;
            this.currentTurnIndex = 0;
        } else {
            this.currentTurnIndex++;
        }
    }

    public prevTurn() {
        const prevState = this.history.pop();
        if (prevState) {
            this.round = prevState.round;
            this.participants = prevState.participants;
            this.currentTurnIndex = prevState.currentTurnIndex;
        }
    }

    private saveHistoryState() {
        this.history.push({
            round: this.round,
            participants: JSON.parse(JSON.stringify(this.participants)),
            currentTurnIndex: this.currentTurnIndex,
        });
    }
}

// Types
type ParticipantInternalType = {
    id: string;
    name: string;
    initiative: number | null;
};

export type PlayerType = ParticipantInternalType & {
    type: 'player';
    character: Character;
};

export type NPCType = ParticipantInternalType & {
    type: 'npc';
    stats: Record<string, unknown>;
};

export type Participant = PlayerType | NPCType;

interface EncounterState {
    round: number;
    participants: Participant[];
    currentTurnIndex: number;
}

export type SerializedEncounter = DTO<Encounter>;

export interface EncounterConfig {
    name: string;
    description: string;
}
