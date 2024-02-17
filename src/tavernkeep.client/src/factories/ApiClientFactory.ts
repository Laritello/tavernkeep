import { AxiosApiClient } from '@/api/axios/AxiosApiClient';
import type { ApiClient } from '@/api/base/ApiClient';

// Just to have one entry point for API creation.
export class ApiClientFactory {
    static createApiClient(): ApiClient {
        return new AxiosApiClient();
    }
}
