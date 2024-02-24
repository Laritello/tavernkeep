import axios, { type AxiosInstance } from 'axios';
import type { ApiClient } from '../base/ApiClient';
import type { ApiResponse } from '../base/ApiResponse';
import { AxiosApiResponse } from './AxiosApiResponse';
import { User } from '@/entities/User';
import { UserRole } from '@/contracts/enums/UserRole';
import { Message, RollMessage, TextMessage } from '@/entities/Message';
import { Character } from '@/entities/Character';
import type { Ability } from '@/contracts/character/Ability';
import type { Skill } from '@/contracts/character/Skill';
import type { AbilityType } from '@/contracts/enums/AbilityType';
import type { Proficiency } from '@/contracts/enums/Proficiency';
import type { SkillType } from '@/contracts/enums/SkillType';
import { plainToInstance } from 'class-transformer';
import type { AuthenticationResponse } from '@/contracts/auth/AuthenticationResponse';
import { useAuthStore } from '@/stores/auth.store';

// TODO: Error handling and interceptors
// TODO: Decorators might be usefull here as I do similar logic every time.
// TODO: Move cookie name somewhere where it will be set globally.
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

        this.client.interceptors.request.use(function (config) {
            const authStore = useAuthStore();
            const token = authStore.getToken();

            if (token)
                config.headers.Authorization = 'Bearer ' + token;
            else
                config.headers.Authorization = null;

            return config;
        });
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

    // TODO: User request types instead of separate parameters
    async createUser(login: string, password: string, role: UserRole): Promise<ApiResponse<User>> {
        const response = await this.client.post<User>(
            'users/create',
            {
                login: login,
                password: password,
                role: role,
            }
        );

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    // TODO: ApiResponse for empty responses
    async deleteUser(id: string): Promise<ApiResponse<null>> {
        const response = await this.client.delete('users/delete/' + id);

        return new AxiosApiResponse(null, response.status, response.statusText);
    }

    async getCharacters(): Promise<ApiResponse<Character[]>> {
        const response = await this.client.get<Character[]>('characters');

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async createCharacter(name: string): Promise<ApiResponse<Character>> {
        const response = await this.client.post<Character>(
            'characters/create',
            { name: name }
        );

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

    async editAbility(characterId: string, type: AbilityType, score: number): Promise<ApiResponse<Ability>> {
        const response = await this.client.patch<Ability>(
            'characters/edit/ability',
            { characterId: characterId, type: type, score: score }
        );

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async editSkill(characterId: string, type: SkillType, proficiency: Proficiency): Promise<ApiResponse<Skill>> {
        const response = await this.client.patch<Skill>(
            'characters/edit/skill',
            { characterId: characterId, type: type, proficiency: proficiency }
        );

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async sendMessage(content: string, recipientId?: string): Promise<ApiResponse<Message>> {
        const response = await this.client.post<Message>(
            'chat/message',
            {
                recipientId: recipientId,
                content: content,
            }
        );

        let data = response.data;

        switch (data.$type) {
            case 'TextMessage':
                data = plainToInstance(TextMessage, data);
                break;
            case 'RollMessage':
                data = plainToInstance(RollMessage, data);
                break;
            default:
                data = plainToInstance(Message, data);
        }

        return new AxiosApiResponse(data, response.status, response.statusText);
    }

    async getMessages(skip: number, take: number): Promise<ApiResponse<Message[]>> {
        const response = await this.client.get<Message[]>('chat', {
            params: { skip: skip, take: take }
        });

        const data = response.data.map((item) => {
            switch (item.$type) {
                case 'TextMessage':
                    return plainToInstance(TextMessage, item);
                case 'RollMessage':
                    return plainToInstance(RollMessage, item);
                default:
                    return plainToInstance(Message, item);
            }
        });

        return new AxiosApiResponse(data, response.status, response.statusText);
    }
}
