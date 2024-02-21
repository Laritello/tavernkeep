import type { User } from './User';

export class Message {
    id: number;
    sender: User;
    recipient?: User;
    created: Date;
    text: string;
    isPrivate: boolean;

    constructor(
        id: number,
        sender: User,
        created: Date,
        text: string,
        isPrivate: boolean,
        recipient?: User,
    ) {
        this.id = id;
        this.sender = sender;
        this.created = created;
        this.text = text;
        this.isPrivate = isPrivate;
        this.recipient = recipient;
    }
}
