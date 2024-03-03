import { Proficiency } from '../enums/Proficiency';

export class Lore {
    topic: string;
    proficiency: Proficiency;
    bonus: number;

    constructor(topic: string, bonus: number) {
        this.topic = topic;
        this.bonus = bonus;
        this.proficiency = Proficiency.Untrained;
    }
}
