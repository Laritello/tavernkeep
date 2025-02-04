import { computed, unref, type MaybeRef } from 'vue';

import type { Character } from '@/entities';
import { useCharacters } from '@/stores/characters';
import { useUsers } from '@/stores/users';

export const useUserAccount = (id: MaybeRef<string | undefined>) => {
    const users = useUsers();
    const charactersStore = useCharacters();

    const userId = unref(id);
    const user = users.get(userId);

    const activeCharacter = computed(() =>
        user.value?.activeCharacterId ? charactersStore.get(user.value.activeCharacterId) : undefined
    );

    const characters = computed(() => {
        return Object.values(charactersStore.all).filter((character) => character.ownerId === userId);
    });

    const assignCharacter = async (characterId: MaybeRef<string>) => {
        if (!user.value) {
            return;
        }

        await charactersStore.assignUserToCharacter(user.value.id, unref(characterId));
    };

    const setActiveCharacter = async (characterId: MaybeRef<string>) => {
        if (!user.value) {
            return;
        }

        user.value.activeCharacterId = unref(characterId);
        await users.setActiveCharacter(user.value.id, user.value.activeCharacterId);
    };

    const createCharacter = async (character: Character): Promise<Character | undefined> => {
        if (!user.value) {
            return;
        }

        return await charactersStore.createCharacter(character);
    };

    const deleteCharacter = async (characterId: MaybeRef<string>) => {
        if (!user.value) {
            return;
        }

        await charactersStore.deleteCharacter(unref(characterId));
    };

    return {
        user,
        characters,
        activeCharacter,
        assignCharacter,
        setActiveCharacter,
        createCharacter,
        deleteCharacter,
    };
};
