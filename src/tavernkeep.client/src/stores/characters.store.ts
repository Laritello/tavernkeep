import type { ApiClient } from '@/api/base/ApiClient';
import type { Character } from '@/entities/Character';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import { defineStore } from 'pinia';
import { ref } from 'vue';

const api: ApiClient = ApiClientFactory.createApiClient();
export const useCharactersStore = defineStore('characters.store', () => {
    const characters = ref<Character[]>([]);

    async function fetchCharacters() {
        const usersResponse = await api.getCharacters();
        characters.value = usersResponse.data;
    }

    async function createCharacter(name: string): Promise<Character | undefined> {
        const response = await api.createCharacter(name);
        if (!response.isSuccess()) {
            console.error(response.statusText);
            return;
        }
        characters.value.push(response.data);
        return response.data;
    }

    async function deleteCharacter(id: string) {
        const response = await api.deleteCharacter(id);
        if (!response.isSuccess()) {
            console.error(response.statusText);
            return;
        }
        const index = characters.value.findIndex((user) => user.id === id);
        characters.value.splice(index, 1);
    }

    async function assingUserToCharacter(userId: string, characterId: string): Promise<Character | undefined> {
        const response = await api.assignUserToCharacter(characterId, userId);
        if (!response.isSuccess()) {
            console.error(response.statusText);
            return;
        }
        return response.data;
    }

    return { characters, fetchCharacters, createCharacter, deleteCharacter, assingUserToCharacter };
});
