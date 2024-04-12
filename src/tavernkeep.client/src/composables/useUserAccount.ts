import { useCharactersStore } from '@/stores/characters.store';
import { useUsersStore } from '@/stores/users';
import { computed, unref, type MaybeRef } from 'vue';

export const useUserAccount = (id: MaybeRef<string | undefined>) => {
    const users = useUsersStore();
    const characters = useCharactersStore();

    const userId = unref(id);
    const user = users.get(userId);

    const activeCharacter = computed(() =>
        user.value?.activeCharacterId ? characters.get(user.value.activeCharacterId) : undefined
    );

    const userCharacters = computed(() => {
        console.log('User characters is updated');
        return Object.values(characters.all).filter((character) => character.ownerId === userId);
    });

    const assignCharacter = async (characterId: MaybeRef<string>) => {
        if (!user.value) {
            console.log('No user selected');
            return;
        }
        await characters.assignUserToCharacter(user.value.id, unref(characterId));
    };

    const setActiveCharacter = async (characterId: MaybeRef<string>) => {
        if (!user.value) {
            console.log('No user selected');
            return;
        }
        user.value.activeCharacterId = unref(characterId);
        await users.setActiveCharacter(user.value.id, user.value.activeCharacterId);
    };

    const createCharacter = async (name: string) => {
        if (!user.value) {
            console.log('No user selected');
            return;
        }

        await characters.createCharacter(user.value.id, name);
    };

    const deleteCharacter = async (characterId: MaybeRef<string>) => {
        if (!user.value) {
            console.log('No user selected');
            return;
        }

        await characters.deleteCharacter(unref(characterId));
    };

    return {
        user,
        userCharacters,
        activeCharacter,
        assignCharacter,
        setActiveCharacter,
        createCharacter,
        deleteCharacter,
    };
};
