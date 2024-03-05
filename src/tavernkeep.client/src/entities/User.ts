import type { UserRole } from '@/contracts/enums/UserRole';
import type { Character } from './Character';

export class User {
    id: string;
    login: string;
    role: UserRole;
    activeCharacter: Character;

    constructor(id: string, login: string, role: UserRole, activeCharacter: Character) {
        this.id = id;
        this.login = login;
        this.role = role;
        this.activeCharacter = activeCharacter;
    }
}
