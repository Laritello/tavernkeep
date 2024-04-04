import { getCookie, setCookie, removeCookie } from 'typescript-cookie';
import { jwtDecode } from 'jwt-decode';
import { ref, computed, watch } from 'vue';
import { defineStore } from 'pinia';

import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { AxiosApiClient } from '@/api/axios/AxiosApiClient';
import type { UserRole } from '@/contracts/enums/UserRole';

// Interface declarations

export interface UserCredentials {
    login: string;
    password: string;
}

type RefreshSuccess = {
    status: 'ok';
    accessToken: string;
    refreshToken: string;
};

type RefreshFailure = {
    status: 'error';
    message: string;
};

export type TokenRefreshResult = RefreshSuccess | RefreshFailure;

// Constants initializations
const cookieName: string = 'tavernkeep.auth.jwt';
const refreshName: string = 'tavernkeep.auth.refresh';

interface JwtToken {
    ['user-id']: string;
    ['user-login']: string;
    ['user-role']: UserRole;
}

export const useAuthStore = defineStore('auth.store', () => {
    const client: AxiosApiClient = ApiClientFactory.createApiClient();
    const cookie = ref<string | undefined>(getCookie(cookieName));
    const refreshCookie = ref<string | undefined>(getCookie(refreshName));

    const token = computed(() => (cookie.value ? jwtDecode<JwtToken>(cookie.value) : undefined));
    const isLoggedIn = computed(() => token.value !== undefined);

    watch(cookie, (value) => {
        if (value) {
            setCookie(cookieName, value);
        } else {
            removeCookie(cookieName);
        }
    });

    watch(refreshCookie, (value) => {
        if (value) {
            setCookie(refreshName, value, { expires: 5 });
        } else {
            removeCookie(refreshName);
        }
    });

    async function login(credentials: UserCredentials) {
        if (isLoggedIn.value) logout();
        const response = await client.auth(credentials.login, credentials.password);

        cookie.value = response.accessToken;
        refreshCookie.value = response.refreshToken;
    }

    let refreshPromise: Promise<TokenRefreshResult> | null = null;
    async function refresh(): Promise<TokenRefreshResult> {
        if (refreshPromise) return refreshPromise;

        const accessToken = cookie.value;
        const refreshToken = refreshCookie.value;

        refreshPromise = client
            .refresh(accessToken!, refreshToken!)
            .then((response): TokenRefreshResult => {
                cookie.value = response.accessToken;
                refreshCookie.value = response.refreshToken;
                return { status: 'ok', accessToken: response.accessToken, refreshToken: response.refreshToken };
            })
            .catch((error: Error): TokenRefreshResult => {
                return { status: 'error', message: error.message };
            })
            .finally(() => {
                refreshPromise = null;
            });

        return refreshPromise;
    }

    async function logout() {
        cookie.value = undefined;
        refreshCookie.value = undefined;
    }

    function havePermissions(requiredRoles?: UserRole[]): Boolean {
        const role = token.value?.['user-role'];
        if (role === undefined) return false;
        if (requiredRoles === undefined) return true;

        return requiredRoles.includes(role);
    }

    async function getAccessToken(): Promise<string | undefined> {
        if (refreshPromise) {
            console.info('[AuthStore.getAccessToken] Waiting for token refresh');
            const refreshResult = await refreshPromise;
            if (refreshResult.status === 'error') {
                throw new Error('Unable to refresh access token: ' + refreshResult.message);
            }

            return refreshResult.accessToken;
        }

        return cookie.value;
    }

    return { isLoggedIn, login, logout, havePermissions, getAccessToken, refresh };
});
