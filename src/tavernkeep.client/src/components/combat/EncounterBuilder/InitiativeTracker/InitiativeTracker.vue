<script setup lang="ts">
import { computed, nextTick, ref } from 'vue';
import { VueDraggable } from 'vue-draggable-plus';

import InitiativeParticipantCard from '@/components/combat/EncounterBuilder/InitiativeTracker/InitiativeParticipantCard.vue';
import type { Encounter } from '@/contracts/encounter/Encounter.ts';
import type { Participant } from '@/contracts/encounter/Participant.ts';

const { encounter } = defineProps<{
    encounter: Encounter;
}>();

const emits = defineEmits<{
    'participants-updated': [value: Participant[]];
    'remove-participant': [value: Participant];
    'next-turn': [];
    'prev-turn': [];
}>();

const drag = ref(false);
const encounterRef = ref(encounter);

const participants = computed({
    get: () => encounterRef.value.participants,
    set: (value) => {
        encounterRef.value.participants = value;
        emits('participants-updated', value);
    },
});

function prevTurn() {
    emits('prev-turn');
}

function nextTurn() {
    emits('next-turn');
}

async function onDragEnd() {
    await nextTick(() => (drag.value = false));
}
</script>

<template>
    <div class="card bg-base-300 shadow-xl">
        <div class="card-body">
            <h2 class="card-title">Initiative Tracker</h2>
            <div class="divider">Initiative Order</div>
            <VueDraggable
                v-model="participants"
                :animation="150"
                handle=".drag-handle"
                class="flex flex-col gap-2 min-h-52"
                @end="onDragEnd"
                @start="drag = true"
            >
                <TransitionGroup :name="drag ? undefined : 'slide'" type="transition">
                    <InitiativeParticipantCard
                        v-for="participant in participants"
                        :key="participant.id"
                        :participant="participant"
                        :active-turn="false"
                        @edit="console.log('edit participant card')"
                        @remove="emits('remove-participant', participant)"
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
.slide-move {
    transition: all 1s ease;
}
</style>
