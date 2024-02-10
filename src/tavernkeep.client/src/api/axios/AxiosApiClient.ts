import axios, { type AxiosInstance } from 'axios';
import type { ApiClient } from '../base/ApiClient';
import type { ApiResponse } from '../base/ApiResponse';
import { AxiosApiResponse } from './AxiosApiResponse';

export class AxiosApiClient implements ApiClient{
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
        await this.client.get<User[]>('users').then(r => {
            console.log(r.status);
        });
        const response = await this.client.get<User[]>('users');
        return new AxiosApiResponse(response.data, response.status, response.statusText);
    }
}