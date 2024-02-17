import type { UserRole } from '@/contracts/enums/UserRole';
import type { Character } from './Character';

export class User {
    id: number;
    login: string;
    role: UserRole;
    characters: Character[];
    activeCharacter: Character;

    constructor(
        id: number,
        login: string,
        role: UserRole,
        characters: Character[],
        acitveCharacter: Character
    ) {
        this.id = id;
        this.login = login;
        this.role = role;
        this.characters = characters;
        this.activeCharacter = acitveCharacter;
    }
}
