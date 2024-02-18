export class Health {
    current: number
    max: number
    temporary: number

    constructor(current: number, max: number, temporary: number) {
        this.current = current;
        this.max = max;
        this.temporary = temporary
    }
}