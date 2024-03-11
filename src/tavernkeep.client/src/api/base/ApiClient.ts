import type { ApiResponse } from './ApiResponse';
import type { AuthenticationResponse } from '@/contracts/auth/AuthenticationResponse';

import type { User, Message, Character } from '@/entities';
import { UserRole, SkillType, Proficiency, AbilityType, RollType } from '@/contracts/enums';
import type { Ability, Skill, Lore } from '@/contracts/character';

export interface ApiClient {
    auth(login: string, password: string): Promise<ApiResponse<AuthenticationResponse>>;
    refresh(accessToken: string, refreshToken: string): Promise<ApiResponse<AuthenticationResponse>>;

    getUsers(): Promise<ApiResponse<User[]>>;
    getCurrentUser(): Promise<ApiResponse<User>>;
    createUser(
        login: string,
        password: string,
        role: UserRole,
        initializeCharacter: boolean,
        characterName?: string
    ): Promise<ApiResponse<User>>;
    editUser(id: string, login: string, password: string, role: UserRole): Promise<ApiResponse<User>>;
    deleteUser(id: string): Promise<ApiResponse<null>>;
    setActiveCharacter(userId: string, characterId: string): Promise<ApiResponse<User>>;

    getCharacters(): Promise<ApiResponse<Character[]>>;
    createCharacter(ownerId: string, name: string): Promise<ApiResponse<Character>>;
    deleteCharacter(id: string): Promise<ApiResponse<null>>;
    getCharacter(id: string): Promise<ApiResponse<Character>>;
    assignUserToCharacter(characterId: string, userId: string): Promise<ApiResponse<Character>>;

    createLore(characterId: string, topic: string, proficiency: Proficiency): Promise<ApiResponse<Lore>>;
    editLore(characterId: string, topic: string, proficiency: Proficiency): Promise<ApiResponse<Lore>>;
    deleteLore(characterId: string, topic: string): Promise<ApiResponse<null>>;

    editAbility(characterId: string, type: AbilityType, score: number): Promise<ApiResponse<Ability>>;
    editSkill(characterId: string, type: SkillType, proficiency: Proficiency): Promise<ApiResponse<Skill>>;

    deleteMessage(messageId: string): Promise<ApiResponse<null>>;
    sendMessage(message: string, recipientId?: string): Promise<ApiResponse<Message>>;
    getMessages(skip: number, take: number): Promise<ApiResponse<Message[]>>;
    sendRollMessage(message: string, rollType: RollType): Promise<ApiResponse<Message>>;
}
