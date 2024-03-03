import type { RollType } from '@/contracts/enums/RollType';
import type { User } from './User';

export class Message {
    id: number;
    sender: User;
    created: Date;
    $type: string

    constructor(
        $type: string,
        id: number,
        sender: User,
        created: Date,
    ) {
        this.$type = $type;
        this.id = id;
        this.sender = sender;
        this.created = created;

    }
}

export class TextMessage extends Message {
    text: string;
    isPrivate: boolean;
    recipient? : User;

    constructor(
        $type: string,
        id: number,
        sender: User,
        created: Date,
        text: string,
        isPrivate: boolean,
        recipient?: User,
        ) {
        super($type, id, sender, created)

        this.text = text;
        this.isPrivate = isPrivate;
        this.recipient = recipient;
    }
}

export class RollMessage extends Message {
    result: RollResult;
    rollType: RollType;
    expression: string;

    constructor(
        $type: string,
        id: number,
        sender: User,
        created: Date,
        result: RollResult,
        rollType: RollType,
        expression: string
    ) {
        super($type, id, sender, created)

        this.result = result;
        this.rollType = rollType;
        this.expression = expression;
    }
}

export class RollResult {
    value: number;
    results: ThrowResult[];

    constructor(value: number, results: ThrowResult[]) {
        this.value = value;
        this.results = results;
    }
}

export class ThrowResult {
    value: number;
    type: string;

    constructor(value: number, type: string) {
        this.value = value;
        this.type = type;
    }
}