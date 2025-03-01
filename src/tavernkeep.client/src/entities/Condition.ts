export interface Condition {
    name: string;
    description: string;
    hasLevels: boolean;
    level: number;
    related: Condition[];
}
