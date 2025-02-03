import { defineStore } from 'pinia';
import { computed, reactive, type MaybeRef, unref } from 'vue';

import type { AxiosApiClient } from '@/api/axios/AxiosApiClient';
import { useSession } from '@/composables/useSession';
import type { UserRole } from '@/contracts/enums/UserRole';
import type { User } from '@/entities/User';
import { ApiClientFactory } from '@/factories/ApiClientFactory';

type Users = Record<string, User>;

const api: AxiosApiClient = ApiClientFactory.createApiClient();
export const useUsers = defineStore('users', () => {
    const dictionary = reactive<Users>({});
    const get = (id: MaybeRef<string | undefined>) => {
        const userId = unref(id);
        return computed(() => (userId ? dictionary[userId] : undefined));
    };

    async function fetch() {
        const session = useSession();
        if (!session.isAuthenticated) return;

        const users = await api.getUsers();
        Object.assign(dictionary, users);
    }

    async function createUser(login: string, password: string, role: UserRole): Promise<void> {
        const user = await api.createUser(login, password, role, false);
        dictionary[user.id] = user;
    }

    async function deleteUser(id: string) {
        await api.deleteUser(id);
        delete dictionary[id];
    }

    async function setActiveCharacter(userId: string, characterId: string): Promise<void> {
        await api.setActiveCharacter(userId, characterId);
        dictionary[userId].activeCharacterId = characterId;
    }

    return {
        dictionary,
        get,
        fetch,
        createUser,
        deleteUser,
        setActiveCharacter,
    };
});
