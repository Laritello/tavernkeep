import type { UserRole } from '@/contracts/enums/UserRole';
import type { Character } from './Character';

export class User {
    id: string;
    login: string;
    role: UserRole;
    characters: Character[];

    constructor(
        id: string,
        login: string,
        role: UserRole,
        characters: Character[]
    ) {
        this.id = id;
        this.login = login;
        this.role = role;
        this.characters = characters;
    }
}
