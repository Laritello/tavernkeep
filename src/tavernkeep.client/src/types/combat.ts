export interface Participant {
    id: string;
    type: 'player' | 'monster';
    name: string;
    initiative: number;
    isActive: boolean;
}

export interface Player extends Participant {
    type: 'player';
    class: string;
    level: number;
    currentHp: number;
    maxHp: number;
}

export interface Monster extends Participant {
    type: 'monster';
    challengeRating: number;
    currentHp: number;
    maxHp: number;
}

export type CombatParticipant = Player | Monster;
