import { storeToRefs } from 'pinia';
import { computed } from 'vue';

import type { UserRole } from '@/contracts/enums';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import { useSessionLocalStorage } from '@/stores/sessionLocalStorage';

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
    const api = ApiClientFactory.createApiClient();
    const sessionData = useSessionLocalStorage();
    const { userId, userRole, userLogin, hasData } = storeToRefs(sessionData);
    const isExpired = computed(() => {
        return Date.now() >= sessionData.expiresAt * 1000;
    });

    const isAuthenticated = computed(() => {
        return hasData.value;
    });

    const refresh = async (): Promise<TokenRefreshResult> => {
        if (!isAuthenticated.value) throw new Error('Not authorized user. Authenticate first');
        if (refreshPromise) return refreshPromise;

        const { accessToken, refreshToken } = sessionData;

        refreshPromise = api
            .refresh(accessToken!, refreshToken!)
            .then((response): TokenRefreshResult => {
                sessionData.setData(response);

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

    const getAccessToken = async (): Promise<string | undefined> => {
        if (refreshPromise) {
            const refreshResult = await refreshPromise;
            if (refreshResult.status === 'error') {
                throw new Error('Unable to refresh access token: ' + refreshResult.message);
            }

            return refreshResult.accessToken;
        }

        return sessionData.accessToken;
    };

    const hasPermissions = (requiredRoles?: UserRole[]): boolean => {
        if (!sessionData.userRole) return false;
        if (!requiredRoles) return true;

        return requiredRoles.includes(sessionData.userRole);
    };

    return { userId, userRole, userLogin, isAuthenticated, isExpired, refresh, getAccessToken, hasPermissions };
};
