import type { UserRole } from '@/contracts/enums';
import type { Character } from './Character';

export interface User {
    id: string;
    login: string;
    role: UserRole;
    activeCharacter: Character;
}
