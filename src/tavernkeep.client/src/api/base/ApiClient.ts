import type { User } from '@/entities/User';
import type { ApiResponse } from './ApiResponse';
import type { UserRole } from '@/contracts/enums/UserRole';
import type { Message } from '@/entities/Message';
import type { Character } from '@/entities/Character';
import type { SkillType } from '@/contracts/enums/SkillType';
import type { Proficiency } from '@/contracts/enums/Proficiency';
import type { Skill } from '@/contracts/character/Skill';
import type { AbilityType } from '@/contracts/enums/AbilityType';
import type { Ability } from '@/contracts/character/Ability';
import type { AuthenticationResponse } from '@/contracts/auth/AuthenticationResponse';
import type { RollType } from '@/contracts/enums/RollType';
import type { Lore } from '@/contracts/character/Lore';

export interface ApiClient {
    auth(login: string, password: string): Promise<ApiResponse<AuthenticationResponse>>;
    refresh(accessToken: string, refreshToken: string): Promise<ApiResponse<AuthenticationResponse>>;

    getUsers(): Promise<ApiResponse<User[]>>;
    getCurrentUser(): Promise<ApiResponse<User>>;
    createUser(login: string, password: string, role: UserRole): Promise<ApiResponse<User>>;
    editUser(id: string, login: string, password: string, role: UserRole): Promise<ApiResponse<User>>;
    deleteUser(id: string): Promise<ApiResponse<null>>;

    getCharacters(): Promise<ApiResponse<Character[]>>;
    createCharacter(name: string): Promise<ApiResponse<Character>>;
    deleteCharacter(id: string): Promise<ApiResponse<null>>;
    getCharacter(id: string): Promise<ApiResponse<Character>>;
    assignUserToCharacter(characterId: string, userId: string): Promise<ApiResponse<Character>>;

    createLore(characterId: string, topic: string, proficiency: Proficiency): Promise<ApiResponse<Lore>>;
    editLore(characterId: string, topic: string, proficiency: Proficiency): Promise<ApiResponse<Lore>>;
    deleteLore(characterId: string, topic: string): Promise<ApiResponse<null>>;

    editAbility(characterId: string, type: AbilityType, score: number): Promise<ApiResponse<Ability>>;
    editSkill(characterId: string, type: SkillType, proficiency: Proficiency): Promise<ApiResponse<Skill>>;

    sendMessage(message: string, recipientId?: string): Promise<ApiResponse<Message>>;
    getMessages(skip: number, take: number): Promise<ApiResponse<Message[]>>;
    sendRollMessage(message: string, rollType: RollType): Promise<ApiResponse<Message>>;
}
