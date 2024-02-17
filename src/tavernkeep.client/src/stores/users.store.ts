import { ref } from 'vue';
import { defineStore } from 'pinia';

import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { ApiClient } from '@/api/base/ApiClient';
import type { User } from '@/entities/User';
import type { UserRole } from '@/contracts/enums/UserRole';

const api: ApiClient = ApiClientFactory.createApiClient();
export const useUsersStore = defineStore('users.store', () => {
    const users = ref<User[]>([]);

    async function fetchUsers() {
        const usersResponse = await api.getUsers();
        users.value = usersResponse.data;
    }

    async function createUser(login: string, password: string, role: UserRole) {
        const response = await api.createUser(login, password, role);
        if (response.isSuccess()) {
            users.value.push(response.data);
        } else {
            console.error(response.statusText);
        }
    }

    async function deleteUser(id: string) {
        const response = await api.deleteUser(id);
        if (response.isSuccess()) {
            const index = users.value.findIndex((user) => user.id === id);
            users.value.splice(index, 1);
        } else {
            console.error(response.statusText);
        }
    }

    return { users, fetchUsers, createUser, deleteUser };
});
