import { computed, ref } from 'vue';
import { defineStore } from 'pinia';

import type { ApiClient } from '@/api/base/ApiClient';
import type { Character } from '@/entities/Character';
import { ApiClientFactory } from '@/factories/ApiClientFactory';

const api: ApiClient = ApiClientFactory.createApiClient();
export const useCharactersStore = defineStore('characters.store', () => {
    const all = ref<Character[]>([]);
    const mapByUserId = computed(() =>
        all.value.reduce((acc, cur) => {
            if (!acc.has(cur.owner.id)) {
                acc.set(cur.owner.id, []);
            }
            acc.get(cur.owner.id)!.push(cur);
            return acc;
        }, new Map<string, Character[]>())
    );

    async function fetch() {
        const usersResponse = await api.getCharacters();
        all.value = usersResponse.data;
    }

    async function createCharacter(ownerId: string, name: string): Promise<Character | undefined> {
        const response = await api.createCharacter(ownerId, name);
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
        console.log(response.data);
        return response.data;
    }

    return {
        all,
        mapByUserId,
        fetch,
        createCharacter,
        deleteCharacter,
        assignUserToCharacter,
    };
});
