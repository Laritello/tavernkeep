import { useSession } from '@/composables/useSession';
import type { AxiosInstance, InternalAxiosRequestConfig } from 'axios';

export default (axiosClient: AxiosInstance) => {
    const response = async (error: any) => {
        const {
            config: originalRequest,
            response: { status },
        } = error;

        if (status !== 401 || originalRequest.isRetry) {
            return Promise.reject(error);
        }

        const session = useSession();
        const refreshResult = await session.refresh();
        if (refreshResult.status === 'error') {
            throw new Error('Unable to refresh access token: ' + refreshResult.message);
        }

        const authorization = `Bearer ${refreshResult.accessToken}`;
        originalRequest.headers.Authorization = authorization;

        originalRequest.isRetry = true;
        return axiosClient(originalRequest);
    };

    const request = async (config: InternalAxiosRequestConfig & { isRetry?: boolean }) => {
        if (config.url?.startsWith('authentication') || config.isRetry) return config;

        let accessToken;
        const session = useSession();

        if (session.isExpired.value) {
            const refreshResult = await session.refresh();
            if (refreshResult.status === 'error')
                throw new Error('Unable to refresh access token: ' + refreshResult.message);
            accessToken = refreshResult.accessToken;
        } else {
            accessToken = await session.getAccessToken();
        }
        config.headers.Authorization = `Bearer ${accessToken}`;

        return config;
    };

    return { request, response };
};
