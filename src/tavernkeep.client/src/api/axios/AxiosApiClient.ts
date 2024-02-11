import axios, { type AxiosInstance } from 'axios';
import type { ApiClient } from '../base/ApiClient';
import type { ApiResponse } from '../base/ApiResponse';
import { AxiosApiResponse } from './AxiosApiResponse';
import type { User } from '@/entities/User';
import type { UserRole } from '@/contracts/enums/UserRole';
import { getCookie } from 'typescript-cookie'

// TODO: Error handling and interceptors
export class AxiosApiClient implements ApiClient {
    client: AxiosInstance;
    private baseURL = 'https://localhost:7231/api/';
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

    async getUsers(): Promise<ApiResponse<User[]>> {
        const response = await this.client.get<User[]>('users');
        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    // TODO: User request types instead of separate parameters
    async createUser(login: string, password: string, role: UserRole): Promise<ApiResponse<User>> {
        const response = await this.client.post<User>('users/create', {
            login: login,
            password: password,
            userRole: role
        }, { headers: { "Authorization": "Bearer " + getCookie(this.cookieName) } });

        return new AxiosApiResponse(response.data, response.status, response.statusText)
    }

    // TODO: ApiResponse for empty responses
    async deleteUser(id: number): Promise<ApiResponse<null>> {
        const response = await this.client.delete('users/delete/' + id,
            { headers: { "Authorization": "Bearer " + getCookie(this.cookieName) } });

        return new AxiosApiResponse(null, response.status, response.statusText);
    }

    async auth(login: string, password: string): Promise<ApiResponse<string>> {
        const response = await this.client.post<string>('authentication/auth', {
            login: login,
            password: password
        });

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }
}