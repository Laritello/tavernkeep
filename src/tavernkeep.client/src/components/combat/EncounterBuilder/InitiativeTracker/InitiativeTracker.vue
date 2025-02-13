<script setup lang="ts">
import { nextTick, ref } from 'vue';
import { VueDraggable } from 'vue-draggable-plus';

import InitiativeParticipantCard from '@/components/InitiativeParticipantCard.vue';
import type { Participant } from '@/contracts/encounter/Participant.ts';
import { useCurrentEncounterStore } from '@/stores/useCurrentEncounterStore.ts';

const currentEncounter = useCurrentEncounterStore();
const drag = ref(false);

async function removeParticipant(participant: Participant) {
    await currentEncounter.removeParticipant(participant);
}

function prevTurn() {
    console.log('Previous turn');
    // currentEncounter.prevTurn();
}

function nextTurn() {
    console.log('Next turn');
    // currentEncounter.nextTurn();
}

async function onDragEnd() {
    await currentEncounter.updateOrder();
    await nextTick(() => (drag.value = false));
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
                @end="onDragEnd"
            >
                <TransitionGroup :name="!drag ? 'fade' : undefined" type="transition">
                    <InitiativeParticipantCard
                        v-for="participant in currentEncounter.participants"
                        :key="participant.id"
                        class="flex items-center justify-between cursor-pointer"
                        :participant="participant"
                        :active-turn="false"
                        @edit="console.log('edit participant card')"
                        @remove="removeParticipant(participant)"
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
