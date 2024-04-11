import { ApiClientFactory } from '@/factories/ApiClientFactory';
import { useSessionLocalStorage, type SessionStorageData } from '@/stores/sessionLocalStorage';

export type UserCredentials = {
    login: string;
    password: string;
};

export const storeKey: string = 'tavernkeep.auth.data';
const api = ApiClientFactory.createApiClient();

export const useAuth = () => {
    const sessionData = useSessionLocalStorage();

    async function login(credentials: UserCredentials): Promise<SessionStorageData> {
        if (sessionData.hasData) logout();
        const response = await api.auth(credentials.login, credentials.password);
        sessionData.setData(response);

        return response;
    }

    function logout() {
        sessionData.setData(undefined);
    }

    return { login, logout };
};
