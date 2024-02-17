import { ApiClientFactory } from '@/factories/ApiClientFactory';
import { ref, computed } from 'vue';
import { defineStore } from 'pinia';
import { getCookie, setCookie, removeCookie } from 'typescript-cookie';
import type { ApiClient } from '@/api/base/ApiClient';

// Interface declarations

export interface UserCredentials {
    login: string;
    password: string;
}

// Constants initializations
const client: ApiClient = ApiClientFactory.createApiClient();
const cookieName: string = 'taverkeep.auth.jwt';

export const useAuthStore = defineStore('auth.store', () => {
    const name = ref<string | undefined>(undefined);
    const isLoggedIn = computed(() => getCookie(cookieName) != undefined);

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

        setCookie(cookieName, response.data, { expires: 7 });
        name.value = credentials.login;
    }

    async function logout() {
        removeCookie(cookieName);
    }

    return { isLoggedIn, name, login, logout };
});
