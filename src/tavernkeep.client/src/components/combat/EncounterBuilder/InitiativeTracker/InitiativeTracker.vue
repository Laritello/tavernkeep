<script setup lang="ts">
import { storeToRefs } from 'pinia';
import { nextTick, ref } from 'vue';
import { VueDraggable } from 'vue-draggable-plus';

import InitiativeParticipantCard from '@/components/InitiativeParticipantCard.vue';
import type { Participant } from '@/entities/Encounter.ts';
import { useEncountersStore } from '@/stores/encountersStore.ts';

const drag = ref(false);

const encountersStore = useEncountersStore();
const { currentEncounter } = storeToRefs(encountersStore);

function removeFromInitiative(participant: Participant) {
    currentEncounter.value.removeParticipant(participant);
}

function prevTurn() {
    currentEncounter.value.prevTurn();
}

function nextTurn() {
    currentEncounter.value.nextTurn();
}
</script>

<template>
    <div class="card bg-base-300 shadow-xl">
        <div class="card-body">
            <h2 class="card-title">Initiative Tracker</h2>
            <div class="divider">Initiative Order</div>
            <VueDraggable
                v-model="currentEncounter.participants"
                class="flex flex-col gap-2 min-h-52"
                :animation="150"
                @start="drag = true"
                @end="nextTick(() => (drag = false))"
            >
                <TransitionGroup :name="!drag ? 'fade' : undefined" type="transition">
                    <InitiativeParticipantCard
                        v-for="creature in currentEncounter.participants"
                        :key="creature.id"
                        class="flex items-center justify-between cursor-pointer"
                        :participant="creature"
                        :active-turn="currentEncounter.getCurrentTurnParticipant()?.id === creature.id"
                        @edit="console.log('edit participant card')"
                        @remove="removeFromInitiative(creature)"
                    />
                </TransitionGroup>
            </VueDraggable>
            <div class="card-actions justify-between mt-4">
                <button class="btn btn-accent" @click="prevTurn">Prev Turn</button>
                <button class="btn btn-accent" @click="nextTurn">Next Turn</button>
            </div>
        </div>
    </div>
</template>

<!--suppress CssUnusedSymbol -->
<style scoped>
.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.15s ease;
}

.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}
</style>
