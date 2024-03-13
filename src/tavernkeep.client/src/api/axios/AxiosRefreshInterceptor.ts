import { useAuthStore } from '@/stores/auth.store';
import type { AxiosInstance } from 'axios';
export default (axiosClient: AxiosInstance) => {
    async function interceptor(error: any) {
        const {
            config: originalRequest,
            response: { status },
        } = error;

        if (status !== 401 || originalRequest._retry) {
            return Promise.reject(error);
        }

        const authStore = useAuthStore();

        const { accessToken } = await authStore.refresh();
        const authorization = `Bearer ${accessToken}`;

        axiosClient.defaults.headers.common.Authorization = authorization;
        originalRequest.headers.Authorization = authorization;

        originalRequest._retry = true;
        return axiosClient(originalRequest);
    }

    axiosClient.interceptors.response.use(undefined, interceptor);
};
