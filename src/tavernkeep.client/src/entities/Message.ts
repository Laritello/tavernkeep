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
    result: number;
    rollType: RollType;

    constructor(
        $type: string,
        id: number,
        sender: User,
        created: Date,
        result: number,
        rollType: RollType
    ) {
        super($type, id, sender, created)

        this.result = result;
        this.rollType = rollType;
    }
}