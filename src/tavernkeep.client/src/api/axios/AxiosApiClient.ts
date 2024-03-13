import axios, { type AxiosInstance } from 'axios';
import AxiosAuthInterceptors from './AxiosAuthInterceptors';
import { AxiosApiResponse } from './AxiosApiResponse';

import type { ApiClient } from '../base/ApiClient';
import type { ApiResponse } from '../base/ApiResponse';
import type { AuthenticationResponse } from '@/contracts/auth/AuthenticationResponse';
import type { User, Message, Character } from '@/entities';
import type { Ability, Skill, Lore } from '@/contracts/character';
import { UserRole, AbilityType, Proficiency, SkillType, RollType } from '@/contracts/enums';

// TODO: Error handling and interceptors
export class AxiosApiClient implements ApiClient {
    client: AxiosInstance;
    private baseURL = 'https://' + window.location.hostname + ':7231/api/';

    constructor() {
        this.client = axios.create({
            baseURL: this.baseURL,
            timeout: 5000, // 5 seconds
            headers: {
                'Content-Type': 'application/json',
            },
        });

        const { request, response } = AxiosAuthInterceptors(this.client);

        this.client.interceptors.request.use(request);
        this.client.interceptors.response.use(undefined, response);
    }

    async auth(login: string, password: string): Promise<ApiResponse<AuthenticationResponse>> {
        const response = await this.client.post<AuthenticationResponse>('authentication/auth', {
            login: login,
            password: password,
        });

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async refresh(accessToken: string, refreshToken: string): Promise<ApiResponse<AuthenticationResponse>> {
        const response = await this.client.post<AuthenticationResponse>('authentication/refresh', {
            accessToken: accessToken,
            refreshToken: refreshToken,
        });

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async getUsers(): Promise<ApiResponse<User[]>> {
        const response = await this.client.get<User[]>('users');
        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async getCurrentUser(): Promise<ApiResponse<User>> {
        const response = await this.client.get<User>('users/current');
        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    // TODO: User request types instead of separate parameters
    async createUser(
        login: string,
        password: string,
        role: UserRole,
        initializeCharacter: boolean,
        characterName?: string
    ): Promise<ApiResponse<User>> {
        const response = await this.client.post<User>('users', {
            login,
            password,
            role,
            initializeCharacter,
            characterName,
        });
        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async editUser(id: string, login: string, password: string, role: UserRole): Promise<ApiResponse<User>> {
        const response = await this.client.patch<User>('users/' + id, {
            login: login,
            password: password,
            role: role,
        });
        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    // TODO: ApiResponse for empty responses
    async deleteUser(id: string): Promise<ApiResponse<null>> {
        const response = await this.client.delete('users/' + id);
        return new AxiosApiResponse(null, response.status, response.statusText);
    }

    async setActiveCharacter(userId: string, characterId: string): Promise<ApiResponse<User>> {
        const response = await this.client.put<User>('users/active-character', {
            userId,
            characterId,
        });
        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async getCharacters(): Promise<ApiResponse<Character[]>> {
        const response = await this.client.get<Character[]>('characters');
        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async createCharacter(ownerId: string, name: string): Promise<ApiResponse<Character>> {
        const response = await this.client.post<Character>('characters/create', { ownerId, name });
        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async deleteCharacter(id: string): Promise<ApiResponse<null>> {
        const response = await this.client.delete('characters/delete/' + id);
        return new AxiosApiResponse(null, response.status, response.statusText);
    }

    async getCharacter(id: string): Promise<ApiResponse<Character>> {
        const response = await this.client.get<Character>('characters/' + id);
        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async assignUserToCharacter(characterId: string, userId: string): Promise<ApiResponse<Character>> {
        const response = await this.client.patch<Character>('characters/assign', {
            characterId: characterId,
            userId: userId,
        });
        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async createLore(characterId: string, topic: string, proficiency: Proficiency): Promise<ApiResponse<Lore>> {
        const response = await this.client.post<Lore>('lore', {
            characterId: characterId,
            topic: topic,
            proficiency: proficiency,
        });

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async editLore(characterId: string, topic: string, proficiency: Proficiency): Promise<ApiResponse<Lore>> {
        const response = await this.client.patch<Lore>('lore', {
            characterId: characterId,
            topic: topic,
            proficiency: proficiency,
        });

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async deleteLore(characterId: string, topic: string): Promise<ApiResponse<null>> {
        const response = await this.client.delete('lore/' + characterId, { params: { topic: topic } });

        return new AxiosApiResponse(null, response.status, response.statusText);
    }

    async editAbility(characterId: string, type: AbilityType, score: number): Promise<ApiResponse<Ability>> {
        const response = await this.client.patch<Ability>('characters/edit/ability', {
            characterId: characterId,
            type: type,
            score: score,
        });

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async editSkill(characterId: string, type: SkillType, proficiency: Proficiency): Promise<ApiResponse<Skill>> {
        const response = await this.client.patch<Skill>('characters/edit/skill', {
            characterId: characterId,
            type: type,
            proficiency: proficiency,
        });

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async sendMessage(content: string, recipientId?: string): Promise<ApiResponse<Message>> {
        const response = await this.client.post<Message>('chat/message', {
            recipientId: recipientId,
            content: content,
        });

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async getMessages(skip: number, take: number): Promise<ApiResponse<Message[]>> {
        const response = await this.client.get<Message[]>('chat', {
            params: { skip: skip, take: take },
        });

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async deleteMessage(messageId: string): Promise<ApiResponse<null>> {
        const response = await this.client.delete<Message[]>(`chat/${messageId}`);
        return new AxiosApiResponse(null, response.status, response.statusText);
    }

    async sendRollMessage(expression: string, rollType: RollType): Promise<ApiResponse<Message>> {
        const response = await this.client.get<Message>('roll/custom', {
            params: { expression, rollType },
        });

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }
}
