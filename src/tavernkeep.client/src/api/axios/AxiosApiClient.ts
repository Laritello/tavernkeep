import axios, { type AxiosInstance } from 'axios';
import type { ApiClient } from '../base/ApiClient';
import type { ApiResponse } from '../base/ApiResponse';
import { AxiosApiResponse } from './AxiosApiResponse';
import { User } from '@/entities/User';
import { UserRole } from '@/contracts/enums/UserRole';
import { getCookie } from 'typescript-cookie'
import { Message } from '@/entities/Message';
import { MessageType } from '@/contracts/enums/MessageType';

// TODO: Error handling and interceptors
// TODO: Decorators might be usefull here as I do similar logic every time.
// TODO: Move cookie name somewhere where it will be set globally.
export class AxiosApiClient implements ApiClient {
    client: AxiosInstance;
    private baseURL = 'https://192.168.0.101:7231/api/';
    private cookieName: string = 'taverkeep.auth.jwt';

    constructor() {
        this.client = axios.create({
            baseURL: this.baseURL,
            timeout: 5000, // 5 seconds
            headers: {
                "Content-Type": "application/json",
            }
        });
    }

    async auth(login: string, password: string): Promise<ApiResponse<string>> {
        const response = await this.client.post<string>('authentication/auth', {
            login: login,
            password: password
        });

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async getUsers(): Promise<ApiResponse<User[]>> {
        const response = await this.client.get<User[]>('users');
        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    // TODO: User request types instead of separate parameters
    async createUser(login: string, password: string, role: UserRole): Promise<ApiResponse<User>> {
        const response = await this.client.post<User>('users/create', {
            login: login,
            password: password,
            role: role
        }, { headers: { "Authorization": "Bearer " + getCookie(this.cookieName) } });

        return new AxiosApiResponse(response.data, response.status, response.statusText)
    }

    // TODO: ApiResponse for empty responses
    async deleteUser(id: number): Promise<ApiResponse<null>> {
        const response = await this.client.delete('users/delete/' + id,
            { headers: { "Authorization": "Bearer " + getCookie(this.cookieName) } });

        return new AxiosApiResponse(null, response.status, response.statusText);
    }

    async sendMessage(content: string, type: MessageType): Promise<ApiResponse<Message>> {
        const response = await this.client.post<Message>('chat', {
            type: type,
            content: content
        }, { headers: { "Authorization": "Bearer " + getCookie(this.cookieName) } })

        return new AxiosApiResponse(response.data, response.status, response.statusText)
    }

    async getMessages(skip: number, take: number): Promise<ApiResponse<Message[]>> {
        const response = await this.client.get<Message[]>('chat', {
            params: { skip: skip, take: take },
            headers: { "Authorization": "Bearer " + getCookie(this.cookieName) }
        })

        return new AxiosApiResponse(response.data, response.status, response.statusText)
    }
}