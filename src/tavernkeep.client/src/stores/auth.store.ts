import { getCookie, setCookie, removeCookie } from 'typescript-cookie';
import { jwtDecode } from 'jwt-decode';
import { ref, computed, watch } from 'vue';
import { defineStore } from 'pinia';

import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { ApiClient } from '@/api/base/ApiClient';
import type { UserRole } from '@/contracts/enums/UserRole';

// Interface declarations

export interface UserCredentials {
    login: string;
    password: string;
}

export interface TokenRefreshResult {
    isSuccess: boolean;
    accessToken: string;
    refreshToken: string;
}

// Constants initializations
const client: ApiClient = ApiClientFactory.createApiClient();
const cookieName: string = 'tavernkeep.auth.jwt';
const refreshName: string = 'tavernkeep.auth.refresh';

interface JwtToken {
    ['user-login']: string;
    ['user-role']: UserRole;
}

export const useAuthStore = defineStore('auth.store', () => {
    const cookie = ref<string | undefined>(getCookie(cookieName));
    const refreshCookie = ref<string | undefined>(getCookie(refreshName));

    const token = computed(() =>
        cookie.value ? jwtDecode<JwtToken>(cookie.value) : undefined
    );
    const userName = computed(() => token.value?.['user-login']);
    const role = computed(() => token.value?.['user-role']);
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

        // Call API for JWT
        const response = await client.auth(
            credentials.login,
            credentials.password
        );

        // TODO: Error handling
        if (!response.isSuccess()) {
            return;
        }

        cookie.value = response.data.accessToken;
        refreshCookie.value = response.data.refreshToken;
    }

    // If anything wrong - throw exception
    async function refresh(): Promise<TokenRefreshResult> {
        const accessToken = cookie.value;
        const refreshToken = refreshCookie.value;

        const response = await client.refresh(accessToken!, refreshToken!);

        if (!response.isSuccess)
            throw new Error('Unable to refresh tokein');

        cookie.value = response.data.accessToken;
        refreshCookie.value = response.data.refreshToken;

        return {
            isSuccess: true,
            accessToken: response.data.accessToken,
            refreshToken: response.data.refreshToken
        };
    }

    async function logout() {
        cookie.value = undefined;
        refreshCookie.value = undefined;
    }

    function havePermissions(requiredRoles?: UserRole[]): Boolean {
        if (requiredRoles === undefined) return true;
        if (role.value === undefined) return false;

        return requiredRoles.includes(role.value);
    }

    function getAccessToken(): string | undefined {
        return cookie.value;
    }

    return { userName, role, isLoggedIn, login, logout, havePermissions, getAccessToken, refresh };
});
