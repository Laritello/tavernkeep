import { computed, ref, reactive, type MaybeRef, unref } from 'vue';
import { defineStore } from 'pinia';

import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { AxiosApiClient } from '@/api/axios/AxiosApiClient';
import type { User } from '@/entities/User';
import type { UserRole } from '@/contracts/enums/UserRole';

import { useAuthStore } from './auth.store';

type Users = Record<string, User>;

const api: AxiosApiClient = ApiClientFactory.createApiClient();
export const useUsersStore = defineStore('users', () => {
    const current = ref<User>();
    const usersDictionary = reactive<Users>({});

    const list = computed(() => Object.values(usersDictionary));
    const listExceptCurrent = computed(() => list.value.filter((user) => user.id !== current.value?.id));
    const currentUser = computed(() => current.value);
    const getById = (id: MaybeRef<string>) => computed(() => usersDictionary[unref(id)]);

    async function fetch() {
        const users = await api.getUsers();
        Object.assign(usersDictionary, users);

        const auth = useAuthStore();
        if (!auth.isLoggedIn) return;

        const currentUserId = auth.currentUserId;
        current.value = usersDictionary[currentUserId];
    }

    async function createUser(login: string, password: string, role: UserRole): Promise<void> {
        const user = await api.createUser(login, password, role, false);
        usersDictionary[user.id] = user;
    }

    async function deleteUser(id: string) {
        await api.deleteUser(id);
        delete usersDictionary[id];
    }

    async function setActiveCharacter(userId: string, characterId: string): Promise<void> {
        await api.setActiveCharacter(userId, characterId);
        usersDictionary[userId].activeCharacterId = characterId;
    }

    return { list, listExceptCurrent, currentUser, getById, fetch, createUser, deleteUser, setActiveCharacter };
});
