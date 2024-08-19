import { reactive } from 'vue';
import { defineStore } from 'pinia';

import type { Character } from '@/entities/Character';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { AxiosApiClient } from '@/api/axios/AxiosApiClient';
import CharacterHub from '@/api/hubs/CharacterHub';

type Characters = Record<string, Character>;

export const useCharacters = defineStore('characters', () => {
    const api: AxiosApiClient = ApiClientFactory.createApiClient();
    const dictionary = reactive<Characters>({});

    CharacterHub.connection.on('OnCharacterEdited', (character: Character) => {
        console.log(character);
        Object.assign(dictionary[character.id], character);
    });

    function get(id: string): Character {
        return dictionary[id];
    }

    async function fetch() {
        const characters = await api.getCharacters();
        Object.assign(dictionary, characters);
    }

    async function createCharacter(ownerId: string, name: string): Promise<Character> {
        const character = await api.createCharacter(ownerId, name);
        dictionary[character.id] = character;

        return character;
    }

    async function deleteCharacter(id: string) {
        await api.deleteCharacter(id);
        delete dictionary[id];
    }

    async function assignUserToCharacter(userId: string, characterId: string): Promise<void> {
        dictionary[characterId] = await api.assignUserToCharacter(characterId, userId);
    }

    return {
        all: dictionary,
        get,
        fetch,
        createCharacter,
        deleteCharacter,
        assignUserToCharacter,
    };
});
