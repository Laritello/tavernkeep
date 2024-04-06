import { useStorage } from '@vueuse/core';
import { storeKey, type JwtTokenData, type SessionStorage } from './useAuth';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import { jwtDecode } from 'jwt-decode';
import { computed } from 'vue';

const api = ApiClientFactory.createApiClient();

type RefreshSuccess = {
    status: 'ok';
    accessToken: string;
    refreshToken: string;
};

type RefreshFailure = {
    status: 'error';
    message: string;
};

type TokenRefreshResult = RefreshSuccess | RefreshFailure;

let refreshPromise: Promise<TokenRefreshResult> | null = null;

export const useSession = () => {
    const token = useStorage<SessionStorage>(storeKey, null);
    const isAuthenticated = computed(() => !!token.value);
    const isExpired = computed(() => {
        if (!token.value) return false;
        return Date.now() >= token.value.expiresAt;
    });

    const refresh = async (): Promise<TokenRefreshResult> => {
        if (!token.value) throw new Error('Not authorized user. Authenticate first');
        if (refreshPromise) return refreshPromise;

        const { accessToken, refreshToken } = token.value;

        refreshPromise = api
            .refresh(accessToken, refreshToken)
            .then((response): TokenRefreshResult => {
                const jwt = jwtDecode<JwtTokenData>(response.accessToken);

                token.value = {
                    ...response,
                    userId: jwt.userId,
                    userLogin: jwt.userLogin,
                    userRole: jwt.userRole,
                    expiresAt: jwt.exp,
                };

                return { status: 'ok', ...response };
            })
            .catch((error: Error): TokenRefreshResult => {
                return { status: 'error', message: error.message };
            })
            .finally(() => {
                refreshPromise = null;
            });

        return refreshPromise;
    };

    return { data: token, isAuthenticated, isExpired, refresh };
};
