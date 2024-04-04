import type { AxiosInstance, InternalAxiosRequestConfig } from 'axios';
import { useAuthStore } from '@/stores/auth.store';
export default (axiosClient: AxiosInstance) => {
    const response = async (error: any) => {
        const {
            config: originalRequest,
            response: { status },
        } = error;

        if (status !== 401 || originalRequest.isRetry) {
            return Promise.reject(error);
        }

        const authStore = useAuthStore();
        const refreshResult = await authStore.refresh();
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

        const authStore = useAuthStore();
        const token = await authStore.getAccessToken();

        if (token) config.headers.Authorization = `Bearer ${token}`;
        else config.headers.Authorization = null;

        return config;
    };

    return { request, response };
};
