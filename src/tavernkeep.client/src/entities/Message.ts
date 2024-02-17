import type { MessageType } from '@/contracts/enums/MessageType';
import type { User } from './User';

export class Message {
    id: number;
    sender: User;
    type: MessageType;
    created: Date;
    content: string;

    constructor(
        id: number,
        sender: User,
        type: MessageType,
        created: Date,
        content: string
    ) {
        this.id = id;
        this.sender = sender;
        this.type = type;
        this.created = created;
        this.content = content;
    }
}
