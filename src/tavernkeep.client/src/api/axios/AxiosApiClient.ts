import axios, { type AxiosInstance, type AxiosRequestConfig } from 'axios';
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
import type { RollType } from '@/contracts/enums/RollType';
import type { Lore } from '@/contracts/character/Lore';

// TODO: Error handling and interceptors
export class AxiosApiClient implements ApiClient {
    client: AxiosInstance;
    private baseURL = 'https://' + window.location.hostname + ':7231/api/';

    // Axios request interceptor to refresh the token on 401 Unauthorized errors
    private isRefreshing: boolean = false; // Flag to track token refresh status
    private refreshSubscribers: ((token: string) => void)[] = []; // Array to hold pending requests while token is refreshing

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
            const token = authStore.getAccessToken();

            if (token) config.headers.Authorization = `Bearer ${token}`;
            else config.headers.Authorization = null;

            return config;
        });

        this.client.interceptors.response.use(
            (response) => response,
            async (error) => {
                if (error.response && error.response.status === 401) {
                    const originalRequest: AxiosRequestConfig = error.config;

                    if (!this.isRefreshing) {
                        this.isRefreshing = true;

                        try {
                            const authStore = useAuthStore();
                            const result = await authStore.refresh();

                            this.onTokenRefreshed(result.accessToken);
                            this.client.defaults.headers.common.Authorization = `Bearer ${result.accessToken}`;
                            originalRequest.headers!.Authorization = `Bearer ${result.accessToken}`;
                            console.log(
                                `Token updated. New token: ${this.client.defaults.headers.common.Authorization}`
                            );

                            return this.client(originalRequest);
                        } catch (refreshError) {
                            // Handle the refresh error, e.g., redirect to the login page
                            console.error('Failed to refresh auth token:', refreshError);

                            const authStore = useAuthStore();
                            authStore.logout();

                            throw refreshError;
                        } finally {
                            this.isRefreshing = false;
                        }
                    } else {
                        // If token refresh is already in progress, wait for the new token
                        return new Promise((resolve) => {
                            this.subscribeTokenRefresh((token) => {
                                originalRequest.headers!.Authorization = `Bearer ${token}`;
                                console.log(
                                    `Updated token without additional calls: ${originalRequest.headers!.Authorization}`
                                );
                                resolve(this.client(originalRequest));
                            });
                        });
                    }
                }

                // For other errors, just pass them through
                throw error;
            }
        );
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
        const data = plainToInstance(Character, response.data);
        return new AxiosApiResponse(data, response.status, response.statusText);
    }

    async createCharacter(ownerId: string, name: string): Promise<ApiResponse<Character>> {
        const response = await this.client.post<Character>('characters/create', { ownerId, name });
        const data = plainToInstance(Character, response.data);
        return new AxiosApiResponse(data, response.status, response.statusText);
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
            params: { skip: skip, take: take },
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

    async sendRollMessage(expression: string, rollType: RollType): Promise<ApiResponse<Message>> {
        const response = await this.client.get<Message>('roll/custom', {
            params: { expression, rollType },
        });

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    // Handling refresh token
    subscribeTokenRefresh(onRefresh: (token: string) => void): void {
        this.refreshSubscribers.push(onRefresh);
    }

    onTokenRefreshed(token: string): void {
        this.refreshSubscribers.forEach((onRefresh) => onRefresh(token));
        this.refreshSubscribers = [];
    }
}
