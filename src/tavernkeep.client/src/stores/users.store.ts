import { computed, ref } from 'vue';
import { defineStore } from 'pinia';

import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { ApiClient } from '@/api/base/ApiClient';
import { type User } from '@/entities/User';
import type { UserRole } from '@/contracts/enums/UserRole';
import { useCharactersStore } from './characters.store';

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
    }

    async function createUser(login: string, password: string, role: UserRole, characterName?: string): Promise<void> {
        const response = await api.createUser(login, password, role, characterName !== null, characterName);
        if (!response.isSuccess()) {
            console.error(response.statusText);
            return;
        }
        console.log(response.data);
        const characterStore = useCharactersStore();
        all.value.push(response.data);
        response.data.activeCharacter.owner = response.data;
        characterStore.all.push(response.data.activeCharacter);
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

        all.value[userIndex].activeCharacter = response.data.activeCharacter;
        if (current.value) current.value.activeCharacter = response.data.activeCharacter;
    }

    return { current, all, other, fetch, createUser, deleteUser, setActiveCharacter };
});
