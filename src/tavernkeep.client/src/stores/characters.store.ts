import { ref } from 'vue';
import { defineStore } from 'pinia';

import type { ApiClient } from '@/api/base/ApiClient';
import type { Character } from '@/entities/Character';
import { ApiClientFactory } from '@/factories/ApiClientFactory';

const api: ApiClient = ApiClientFactory.createApiClient();
export const useCharactersStore = defineStore('characters.store', () => {
    const all = ref<Character[]>([]);

    async function fetch() {
        const usersResponse = await api.getCharacters();
        all.value = usersResponse.data;
    }

    async function createCharacter(name: string): Promise<Character | undefined> {
        const response = await api.createCharacter(name);
        if (!response.isSuccess()) {
            console.error(response.statusText);
            return;
        }
        all.value.push(response.data);
        return response.data;
    }

    async function deleteCharacter(id: string) {
        const response = await api.deleteCharacter(id);
        if (!response.isSuccess()) {
            console.error(response.statusText);
            return;
        }
        const index = all.value.findIndex((user) => user.id === id);
        all.value.splice(index, 1);
    }

    async function assignUserToCharacter(userId: string, characterId: string): Promise<Character | undefined> {
        const response = await api.assignUserToCharacter(characterId, userId);
        if (!response.isSuccess()) {
            console.error(response.statusText);
            return;
        }
        return response.data;
    }

    return {
        all,
        fetch,
        createCharacter,
        deleteCharacter,
        assignUserToCharacter,
    };
});
