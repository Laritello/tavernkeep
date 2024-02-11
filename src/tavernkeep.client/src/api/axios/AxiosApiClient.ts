import axios, { type AxiosInstance } from 'axios';
import type { ApiClient } from '../base/ApiClient';
import type { ApiResponse } from '../base/ApiResponse';
import { AxiosApiResponse } from './AxiosApiResponse';

// TODO: Error handling and interceptors
export class AxiosApiClient implements ApiClient {
    client: AxiosInstance;
    private baseURL = 'https://192.168.0.103:7231/api/';

    constructor() {
        this.client = axios.create({
            baseURL: this.baseURL,
            timeout: 5000, // 5 seconds
            headers: {
                "Content-Type": "application/json",
            }
        });
    }

    async getUsers() : Promise<ApiResponse<User[]>> {
        const response = await this.client.get<User[]>('users');
        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }

    async auth(login: string, password: string) : Promise<ApiResponse<string>> {
        const response = await this.client.post<string>('authentication/auth', {
            login: login,
            password: password
        });

        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }
}