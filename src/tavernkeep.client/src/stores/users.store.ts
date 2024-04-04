import { computed, ref } from 'vue';
import { defineStore } from 'pinia';

import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { AxiosApiClient } from '@/api/axios/AxiosApiClient';
import type { User } from '@/entities/User';
import type { UserRole } from '@/contracts/enums/UserRole';

import { useAuthStore } from './auth.store';

const api: AxiosApiClient = ApiClientFactory.createApiClient();
export const useUsersStore = defineStore('users.store', () => {
    const current = ref<User>();
    const all = ref<Record<string, User>>({});
    const other = computed(() => Object.values(all.value).filter((user) => user.id !== current.value?.id));

    async function fetch() {
        all.value = await api.getUsers();

        const auth = useAuthStore();
        if (!auth.isLoggedIn) return;

        const currentUserId = auth.currentUserId;
        current.value = all.value[currentUserId];
    }

    async function createUser(login: string, password: string, role: UserRole): Promise<void> {
        const user = await api.createUser(login, password, role, false);
        all.value[user.id] = user;
    }

    async function deleteUser(id: string) {
        await api.deleteUser(id);
        delete all.value[id];
    }

    async function setActiveCharacter(userId: string, characterId: string): Promise<void> {
        await api.setActiveCharacter(userId, characterId);
        all.value[userId].activeCharacterId = characterId;
    }

    return { current, all, other, fetch, createUser, deleteUser, setActiveCharacter };
});
