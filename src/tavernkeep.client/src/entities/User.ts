import type { UserRole } from '@/contracts/enums/UserRole';
import { Character } from './Character';
import { Type } from 'class-transformer';

export class User {
    id: string;
    login: string;
    role: UserRole;

    @Type(() => Character)
    activeCharacter: Character;

    constructor(id: string, login: string, role: UserRole, activeCharacter: Character) {
        this.id = id;
        this.login = login;
        this.role = role;
        this.activeCharacter = activeCharacter;
    }
}
