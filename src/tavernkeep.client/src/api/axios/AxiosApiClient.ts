import axios, { type AxiosInstance, type AxiosResponse } from 'axios';
import AxiosAuthInterceptors from './AxiosAuthInterceptors';

import type { AuthenticationResponse } from '@/contracts/auth/AuthenticationResponse';
import type { User, Message, Character } from '@/entities';
import type { Ability, Skill, Lore, Perception } from '@/contracts/character';
import { UserRole, AbilityType, Proficiency, SkillType, RollType, SavingThrowType } from '@/contracts/enums';
import type { Condition } from '@/entities/Condition';

export class AxiosApiClient {
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

    async auth(login: string, password: string): Promise<AuthenticationResponse> {
        const response = await this.client.post<AuthenticationResponse>('authentication/auth', {
            login: login,
            password: password,
        });

        return getPayloadOrThrow(response);
    }

    async refresh(accessToken: string, refreshToken: string): Promise<AuthenticationResponse> {
        const response = await this.client.post<AuthenticationResponse>('authentication/refresh', {
            accessToken: accessToken,
            refreshToken: refreshToken,
        });

        return getPayloadOrThrow(response);
    }

    async getUsers(): Promise<Record<string, User>> {
        const response = await this.client.get<Record<string, User>>('users');
        return getPayloadOrThrow(response);
    }

    async getCurrentUser(): Promise<User> {
        const response = await this.client.get<User>('users/current');
        return getPayloadOrThrow(response);
    }

    // TODO: User request types instead of separate parameters
    async createUser(
        login: string,
        password: string,
        role: UserRole,
        initializeCharacter: boolean,
        characterName?: string
    ): Promise<User> {
        const response = await this.client.post<User>('users', {
            login,
            password,
            role,
            initializeCharacter,
            characterName,
        });
        return getPayloadOrThrow(response);
    }

    async editUser(id: string, login: string, password: string, role: UserRole): Promise<User> {
        const response = await this.client.patch<User>('users/' + id, {
            login: login,
            password: password,
            role: role,
        });
        return getPayloadOrThrow(response);
    }

    async deleteUser(id: string): Promise<void> {
        const response = await this.client.delete('users/' + id);
        return getPayloadOrThrow(response);
    }

    async setActiveCharacter(userId: string, characterId: string): Promise<User> {
        const response = await this.client.put<User>('users/active-character', {
            userId,
            characterId,
        });
        return getPayloadOrThrow(response);
    }

    async getCharacters(): Promise<Record<string, Character>> {
        const response = await this.client.get<Record<string, Character>>('characters');
        return getPayloadOrThrow(response);
    }

    async createCharacter(ownerId: string, name: string): Promise<Character> {
        const response = await this.client.post<Character>('characters/create', { ownerId, name });
        return getPayloadOrThrow(response);
    }

    async deleteCharacter(id: string): Promise<void> {
        const response = await this.client.delete('characters/delete/' + id);
        return getPayloadOrThrow(response);
    }

    async getCharacter(id: string): Promise<Character> {
        const response = await this.client.get<Character>('characters/' + id);
        return getPayloadOrThrow(response);
    }

    async assignUserToCharacter(characterId: string, userId: string): Promise<Character> {
        const response = await this.client.patch<Character>('characters/assign', {
            characterId: characterId,
            userId: userId,
        });
        return getPayloadOrThrow(response);
    }

    async createLore(characterId: string, topic: string, proficiency: Proficiency): Promise<Lore> {
        const response = await this.client.post<Lore>('lore', {
            characterId: characterId,
            topic: topic,
            proficiency: proficiency,
        });

        return getPayloadOrThrow(response);
    }

    async editLore(characterId: string, topic: string, proficiency: Proficiency): Promise<Lore> {
        const response = await this.client.patch<Lore>('lore', {
            characterId: characterId,
            topic: topic,
            proficiency: proficiency,
        });

        return getPayloadOrThrow(response);
    }

    async deleteLore(characterId: string, topic: string): Promise<void> {
        const response = await this.client.delete('lore/' + characterId, { params: { topic: topic } });

        return getPayloadOrThrow(response);
    }

    async editAbility(characterId: string, type: AbilityType, score: number): Promise<Ability> {
        const response = await this.client.patch<Ability>('characters/edit/ability', {
            characterId: characterId,
            type: type,
            score: score,
        });

        return getPayloadOrThrow(response);
    }

    async editSkill(characterId: string, type: SkillType, proficiency: Proficiency): Promise<Skill> {
        const response = await this.client.patch<Skill>('characters/edit/skill', {
            characterId: characterId,
            type: type,
            proficiency: proficiency,
        });

        return getPayloadOrThrow(response);
    }

    async editSavingThrow(
        characterId: string,
        type: SavingThrowType,
        proficiency: Proficiency
    ): Promise<SavingThrowType> {
        const response = await this.client.patch<Skill>('characters/edit/saving-throw', {
            characterId: characterId,
            type: type,
            proficiency: proficiency,
        });

        return getPayloadOrThrow(response);
    }

    async editPerception(characterId: string, proficiency: Proficiency): Promise<Perception> {
        const response = await this.client.patch<Perception>('characters/edit/perception', {
            characterId: characterId,
            proficiency: proficiency,
        });

        return getPayloadOrThrow(response);
    }

    async sendMessage(content: string, recipientId?: string): Promise<Message> {
        const response = await this.client.post<Message>('chat/message', {
            recipientId: recipientId,
            content: content,
        });

        return getPayloadOrThrow(response);
    }

    async getMessages(skip: number, take: number): Promise<Message[]> {
        const response = await this.client.get<Message[]>('chat', {
            params: { skip: skip, take: take },
        });

        return getPayloadOrThrow(response);
    }

    async deleteMessage(messageId: string): Promise<void> {
        const response = await this.client.delete<Message[]>(`chat/${messageId}`);
        return getPayloadOrThrow(response);
    }

    async sendRollMessage(expression: string, rollType: RollType): Promise<Message> {
        const response = await this.client.get<Message>('roll/custom', {
            params: { expression, rollType },
        });

        return getPayloadOrThrow(response);
    }

    async getConditions(): Promise<Condition[]> {
        const response = await this.client.get<Condition[]>('conditions');

        return getPayloadOrThrow(response);
    }

    async applyCondition(characterId: string, conditionName: string, conditionLevel: number): Promise<Character> {
        const response = await this.client.post<Character>('conditions/apply', {
            characterId: characterId,
            conditionName: conditionName,
            conditionLevel: conditionLevel,
        });

        return getPayloadOrThrow(response);
    }

    async removeCondition(characterId: string, conditionName: string): Promise<Character> {
        const response = await this.client.delete<Character>('conditions/remove', {
            data: {
                characterId: characterId,
                conditionName: conditionName,
            },
        });

        return getPayloadOrThrow(response);
    }
}

function getPayloadOrThrow<T>(response: AxiosResponse): T {
    if (response.status < 100 || response.status >= 400) {
        throw new Error(response.statusText);
    }

    return response.data;
}
