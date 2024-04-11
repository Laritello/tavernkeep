import { ApiClientFactory } from '@/factories/ApiClientFactory';
import { computed } from 'vue';
import type { UserRole } from '@/contracts/enums';
import { useSessionLocalStorage } from '@/stores/sessionLocalStorage';
import { storeToRefs } from 'pinia';

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
    const sessionData = useSessionLocalStorage();
    const { userId, userRole, userLogin, hasData: isAuthenticated } = storeToRefs(sessionData);
    const isExpired = computed(() => {
        if (!isAuthenticated.value) return false;
        return Date.now() >= sessionData.expiresAt * 1000;
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

    const havePermissions = (requiredRoles?: UserRole[]): Boolean => {
        const role = sessionData.userRole ?? undefined;
        if (role === undefined) return false;
        if (requiredRoles === undefined) return true;

        return requiredRoles.includes(role);
    };

    return { userId, userRole, userLogin, isAuthenticated, isExpired, refresh, getAccessToken, havePermissions };
};
