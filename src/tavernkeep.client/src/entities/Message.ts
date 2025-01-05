import type { RollType } from '@/contracts/enums';
import type { User } from './User';
import type { Skill } from '@/contracts/character';

export interface Message {
    id: string;
    displayName:string;
    sender: User;
    created: Date;
    $type: string;
}

export interface TextMessage extends Message {
    text: string;
    isPrivate: boolean;
    recipient?: User;
}

export interface RollMessage extends Message {
    result: RollResult;
    rollType: RollType;
    expression: string;
}

export interface SkillRollMessage extends RollMessage {
    skill: Skill;
}

export interface RollResult {
    value: number;
    modifier: number;
    results: ThrowResult[];
}

export interface ThrowResult {
    value: number;
    type: string;
}
