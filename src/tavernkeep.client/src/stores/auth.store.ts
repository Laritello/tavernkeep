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

// Constants initializations
const client: ApiClient = ApiClientFactory.createApiClient();
const cookieName: string = 'taverkeep.auth.jwt';

interface JwtToken {
    ['user-login']: string;
    ['user-role']: UserRole;
}

export const useAuthStore = defineStore('auth.store', () => {
    const cookie = ref<string | undefined>(getCookie(cookieName));
    const token = computed(() =>
        cookie.value ? jwtDecode<JwtToken>(cookie.value) : undefined
    );
    const userName = computed(() => token.value?.['user-login']);
    const role = computed(() => token.value?.['user-role']);
    const isLoggedIn = computed(() => token.value !== undefined);

    watch(cookie, (value) => {
        if (value) {
            setCookie(cookieName, value, { expires: 7 });
        } else {
            removeCookie(cookieName);
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

        cookie.value = response.data;
    }

    async function logout() {
        cookie.value = undefined;
    }

    function havePermissions(requiredRoles?: UserRole[]): Boolean {
        if (requiredRoles === undefined) return true;
        if (role.value === undefined) return false;

        return requiredRoles.includes(role.value);
    }

    return { userName, role, isLoggedIn, login, logout, havePermissions };
});
