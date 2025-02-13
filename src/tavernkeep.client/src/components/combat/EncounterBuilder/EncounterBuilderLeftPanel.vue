<script setup lang="ts">
import { ParticipantType } from '@/contracts/enums';
import type { Character } from '@/entities';
import { useCharacters } from '@/stores/characters.ts';
import { useCurrentEncounterStore } from '@/stores/useCurrentEncounterStore.ts';

const currentEncounterStore = useCurrentEncounterStore();
const charactersStore = useCharacters();

async function addPlayerCharacter(character: Character) {
    if (!currentEncounterStore.isActive) {
        return;
    }

    await currentEncounterStore.addParticipant({
        type: ParticipantType.Character,
        entityId: character.id,
    });
}
</script>

<template>
    <aside class="bg-base-100">
        <div class="m-4">
            <h1 class="text-xl font-semibold">Characters</h1>
            <ul class="menu gap-2">
                <li
                    v-for="character in charactersStore.all"
                    :key="character.id"
                    class="flex flex-row gap-2 items-baseline"
                >
                    <span class="text-lg">{{ character.name }}</span>
                    <span
                        class="btn btn-circle btn-sm mdi mdi-chevron-right"
                        :class="{ 'btn-disabled': !currentEncounterStore.isActive }"
                        @click="addPlayerCharacter(character)"
                    ></span>
                </li>
            </ul>
        </div>
    </aside>
</template>

<style scoped></style>
