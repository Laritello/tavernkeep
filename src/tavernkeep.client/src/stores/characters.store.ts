import type { ApiClient } from "@/api/base/ApiClient";
import type { Character } from "@/entities/Character";
import { ApiClientFactory } from "@/factories/ApiClientFactory";
import { defineStore } from "pinia";
import { ref } from "vue";

const api: ApiClient = ApiClientFactory.createApiClient();
export const useCharactersStore = defineStore('characters.store', () => {
    const characters = ref<Character[]>([]);

    async function fetchCharacters() {
        const usersResponse = await api.getCharacters();
        characters.value = usersResponse.data;
    }

    async function createCharacter(name: string) {
        const response = await api.createCharacter(name);
        if (response.isSuccess()) {
            characters.value.push(response.data);
        } else {
            console.error(response.statusText);
        }
    }

    async function deleteCharacter(id: string) {
        const response = await api.deleteCharacter(id);
        if (response.isSuccess()) {
            const index = characters.value.findIndex((user) => user.id === id);
            characters.value.splice(index, 1);
        } else {
            console.error(response.statusText);
        }
    }

    return { users: characters, fetchCharacters, createCharacter, deleteCharacter };
});