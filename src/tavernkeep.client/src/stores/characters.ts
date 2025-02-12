import { defineStore } from 'pinia';
import { reactive } from 'vue';

import type { AxiosApiClient } from '@/api/axios/AxiosApiClient';
import CharacterHub from '@/api/hubs/CharacterHub';
import type { Character } from '@/entities/Character';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import EncounterHub from '@/api/hubs/EncounterHub';
import type { Encounter } from '@/entities/Encounter';

type Characters = Record<string, Character>;

export const useCharacters = defineStore('characters', () => {
    const api: AxiosApiClient = ApiClientFactory.createApiClient();
    const dictionary = reactive<Characters>({});

    CharacterHub.connection.on('OnCharacterCreated', (character: Character) => {
        Object.assign(dictionary[character.id], character);
    });

    CharacterHub.connection.on('OnCharacterEdited', (character: Character) => {
        Object.assign(dictionary[character.id], character);
    });

    CharacterHub.connection.on('OnCharacterDeleted', (characterId: string) => {
        console.log(characterId);
    });

    // TODO: Move it to encounter store :)
    EncounterHub.connection.on('OnEncounterCreated', (encounter: Encounter) => {
        console.log(encounter);
    });

    EncounterHub.connection.on('OnEncounterUpdated', (encounter: Encounter) => {
        console.log(encounter);
    });

    EncounterHub.connection.on('OnEncounterDeleted', (encounterId: string) => {
        console.log(encounterId);
    });

    EncounterHub.connection.on('OnEncounterLaunched', (encounterId: string) => {
        console.log(encounterId);
    });

    function get(id: string): Character {
        return dictionary[id];
    }

    async function fetch() {
        const characters = await api.getCharacters();
        Object.assign(dictionary, characters);
    }

    async function createCharacter(character: Character): Promise<Character> {
        const createdCharacter = await api.createCharacter(character);
        dictionary[createdCharacter.id] = createdCharacter;
        return createdCharacter;
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
