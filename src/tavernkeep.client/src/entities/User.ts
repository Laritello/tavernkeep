import type { UserRole } from '@/contracts/enums';

export interface User {
    id: string;
    login: string;
    role: UserRole;
    activeCharacterId: string;
    charactersId: string[];
}
