import { computed, ref } from 'vue';
import { defineStore } from 'pinia';

import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { ApiClient } from '@/api/base/ApiClient';
import { User } from '@/entities/User';
import type { UserRole } from '@/contracts/enums/UserRole';

const api: ApiClient = ApiClientFactory.createApiClient();
export const useUsersStore = defineStore('users.store', () => {
    const current = ref<User | undefined>();
    const all = ref<User[]>([]);
    const other = computed(() => all.value.filter((user) => user.id !== current.value?.id));

    async function fetch() {
        const usersResponse = await api.getUsers();
        all.value = usersResponse.data;
        const userResponse = await api.getCurrentUser();
        current.value = userResponse.data;

        console.log(all.value);
    }

    async function createUser(login: string, password: string, role: UserRole) {
        const response = await api.createUser(login, password, role);
        if (response.isSuccess()) {
            all.value.push(response.data);
        } else {
            console.error(response.statusText);
        }
    }

    async function deleteUser(id: string) {
        const response = await api.deleteUser(id);
        if (response.isSuccess()) {
            const index = all.value.findIndex((user) => user.id === id);
            all.value.splice(index, 1);
        } else {
            console.error(response.statusText);
        }
    }

    async function setActiveCharacter(userId: string, characterId: string): Promise<void> {
        const response = await api.setActiveCharacter(userId, characterId);

        if (!response.isSuccess()) {
            console.error(response.statusText);
            return;
        }

        const userIndex = all.value.findIndex((user) => user.id === userId);
        if (userIndex < 0) {
            console.warn(`User '${userId}' not found`);
            return;
        }

        console.log(all.value[userIndex].login, response.data.activeCharacter.name);

        all.value[userIndex].activeCharacter = response.data.activeCharacter;
    }

    return { current, all, other, fetch, createUser, deleteUser, setActiveCharacter };
});
