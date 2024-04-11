import { ref } from 'vue';
import { defineStore } from 'pinia';

import type { Character } from '@/entities/Character';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { AxiosApiClient } from '@/api/axios/AxiosApiClient';

const api: AxiosApiClient = ApiClientFactory.createApiClient();
export const useCharactersStore = defineStore('characters', () => {
    const all = ref<Record<string, Character>>({});

    function get(id: string): Character {
        return all.value[id];
    }

    async function fetch() {
        all.value = await api.getCharacters();
    }

    async function createCharacter(ownerId: string, name: string): Promise<Character> {
        const character = await api.createCharacter(ownerId, name);
        all.value[character.id] = character;

        return character;
    }

    async function deleteCharacter(id: string) {
        await api.deleteCharacter(id);
        delete all.value[id];
    }

    async function assignUserToCharacter(userId: string, characterId: string): Promise<Character> {
        const character = await api.assignUserToCharacter(characterId, userId);
        all.value[characterId] = character;
        // TODO: update character owner
        return character;
    }

    return {
        all,
        get,
        fetch,
        createCharacter,
        deleteCharacter,
        assignUserToCharacter,
    };
});
