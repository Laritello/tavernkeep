import { computed, type MaybeRef } from 'vue';
import { useCharactersStore } from '@/stores/characters.store';
import { useUsersStore } from '@/stores/users.store';

export function useCharacter(characterId: string) {
    const charactersStore = useCharactersStore();
    const character = computed(() => charactersStore.all[characterId]);

    return {
        character,
    };
}

export function useActiveCharacter(userId: MaybeRef<string>) {
    const usersStore = useUsersStore();
    const charactersStore = useCharactersStore();

    const user = usersStore.getById(userId);
    const character = computed(() => {
        if (user.value?.activeCharacterId in charactersStore.all) {
            return charactersStore.all[user.value.activeCharacterId];
        }

        return undefined;
    });

    return {
        character,
    };
}
