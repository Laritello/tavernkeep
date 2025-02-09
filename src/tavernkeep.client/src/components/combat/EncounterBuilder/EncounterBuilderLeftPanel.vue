<script setup lang="ts">
import { storeToRefs } from 'pinia';

import type { Participant } from '@/entities/Encounter.ts';
import { useEncountersStore } from '@/stores/encountersStore.ts';

const encountersStore = useEncountersStore();
const { characters, currentEncounter } = storeToRefs(encountersStore);

function addInitiativeParticipant(player: Participant) {
    if (!currentEncounter.value) {
        return;
    }

    currentEncounter.value.addParticipant(player);
}
</script>

<template>
    <aside class="bg-base-100">
        <div class="m-4">
            <h1 class="text-xl font-semibold">Characters</h1>
            <ul class="menu gap-2">
                <li v-for="character in characters" :key="character.id" class="flex flex-row gap-2 items-baseline">
                    <span class="text-lg">{{ character.name }}</span>
                    <span
                        class="btn btn-circle btn-sm mdi mdi-chevron-right"
                        :class="{ 'btn-disabled': !currentEncounter }"
                        @click="addInitiativeParticipant(character)"
                    ></span>
                </li>
            </ul>
        </div>
    </aside>
</template>

<style scoped></style>
