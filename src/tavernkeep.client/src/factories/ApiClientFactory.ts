import { AxiosApiClient } from '@/api/axios/AxiosApiClient';

// Just to have one entry point for API creation.
export class ApiClientFactory {
    static createApiClient(): AxiosApiClient {
        return new AxiosApiClient();
    }
}
