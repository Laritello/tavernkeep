import type { RollType } from '@/contracts/enums';
import type { User } from './User';
import type { SavingThrow, Skill } from '@/contracts/character';

export interface Message {
    id: string;
    displayName:string;
    sender: User;
    created: Date;
    $type: 'TextMessage' | 'RollMessage' | 'SkillRollMessage' | 'SavingThrowRollMessage';
}

export interface TextMessage extends Message {
    $type: 'TextMessage';
    text: string;
    isPrivate: boolean;
    recipient?: User;
}

export interface RollMessage extends Message {
    $type: 'RollMessage' | 'SkillRollMessage' | 'SavingThrowRollMessage';
    result: RollResult;
    rollType: RollType;
    expression: string;
}

export interface SkillRollMessage extends RollMessage {
    $type: 'SkillRollMessage'
    skill: Skill;
}

export interface SavingThrowRollMessage extends RollMessage {
    $type: 'SavingThrowRollMessage'
    savingThrow: SavingThrow;
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
