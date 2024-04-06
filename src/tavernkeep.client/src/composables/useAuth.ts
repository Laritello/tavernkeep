import type { UserRole } from '@/contracts/enums';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import { useStorage } from '@vueuse/core';
import { jwtDecode } from 'jwt-decode';

export type JwtTokenData = {
    id: string;
    userId: string;
    userLogin: string;
    userRole: UserRole;
    exp: number;
};

export type UserCredentials = {
    login: string;
    password: string;
};

export type SessionStorage = {
    accessToken: string;
    refreshToken: string;
    userId: string;
    userLogin: string;
    userRole: UserRole;
    expiresAt: number;
};

export const storeKey: string = 'tavernkeep.auth.data';
const api = ApiClientFactory.createApiClient();

export const useAuth = () => {
    const token = useStorage<SessionStorage>(storeKey, null);

    async function login(credentials: UserCredentials): Promise<SessionStorage> {
        if (token.value) logout();
        const response = await api.auth(credentials.login, credentials.password);
        const jwt = jwtDecode<JwtTokenData>(response.accessToken);

        token.value = {
            ...response,
            userId: jwt.userId,
            userLogin: jwt.userLogin,
            userRole: jwt.userRole,
            expiresAt: jwt.exp,
        };

        return token.value;
    }

    function logout() {
        token.value = null;
    }

    return { login, logout };
};
