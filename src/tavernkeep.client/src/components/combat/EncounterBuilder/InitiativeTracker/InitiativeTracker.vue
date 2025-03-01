<script setup lang="ts">
import { computed, nextTick, ref } from 'vue';
import { VueDraggable } from 'vue-draggable-plus';

import InitiativeParticipantCard from '@/components/combat/EncounterBuilder/InitiativeTracker/InitiativeParticipantCard.vue';
import type { Encounter } from '@/contracts/encounter/Encounter.ts';
import { EncounterStateType } from '@/contracts/encounter/EncounterStateType.ts';
import type { Participant } from '@/contracts/encounter/Participant.ts';
import { ApiClientFactory } from '@/factories/ApiClientFactory.ts';

const api = ApiClientFactory.createApiClient();

const { encounter } = defineProps<{
    encounter: Encounter;
}>();

const emits = defineEmits<{
    'participants-updated': [value: Participant[]];
    'remove-participant': [value: Participant];
    'next-turn': [];
    'prev-turn': [];
    'begin-encounter': [];
    'end-encounter': [];
    'roll-initiative': [value: boolean];
    'reset-initiative': [];
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

async function onAddCondition(targetId: string, conditionName: string) {
    await api.applyConditionToParticipant(encounter.id, targetId, conditionName);
}

async function onRemoveCondition(targetId: string, conditionName: string) {
    await api.removeConditionFromParticipant(encounter.id, targetId, conditionName);
}

async function onEditCondition(targetId: string, conditionName: string, conditionLevel: number) {
    await api.editConditionOnParticipant(encounter.id, targetId, conditionName, conditionLevel);
}
</script>

<template>
    <div class="mx-4">
        <div class="flex flex-col bg-base-200 rounded-md shadow-xl gap-4">
            <div class="flex flex-row justify-between p-2 border-b-[1px] border-base-300">
                <h1 class="text-lg font-semibold">Initiative Tracker</h1>
                <div class="flex gap-1 justify-self-end">
                    <div class="join">
                        <button class="join-item btn btn-sm btn-primary" @click="emits('roll-initiative', true)">
                            <span class="mdi mdi-dice-d20"></span>
                            Roll initiative
                        </button>
                        <div class="dropdown dropdown-end">
                            <div tabindex="0" role="button" class="join-item btn btn-sm btn-neutral">
                                <span class="mdi mdi-chevron-down"></span>
                            </div>
                            <ul
                                tabindex="0"
                                class="dropdown-content menu bg-base-100 rounded-box z-[1] w-52 p-2 shadow"
                            >
                                <li><a @click="emits('roll-initiative', false)">Roll for all</a></li>
                                <li><a @click="emits('reset-initiative')">Reset initiative</a></li>
                            </ul>
                        </div>
                    </div>
                    <button
                        v-if="encounterRef.status === EncounterStateType.Draft"
                        class="btn btn-sm btn-primary"
                        @click="emits('begin-encounter')"
                    >
                        <span class="mdi mdi-flag"></span>
                        Start
                    </button>
                    <button
                        v-else-if="encounterRef.status === EncounterStateType.Active"
                        class="btn btn-sm btn-primary"
                        @click="emits('end-encounter')"
                    >
                        <span class="mdi mdi-flag-checkered"></span>
                        Finish
                    </button>
                    <button v-else class="btn btn-sm btn-primary" @click="emits('begin-encounter')">
                        <span class="mdi mdi-restart"></span>
                        Restart
                    </button>
                </div>
            </div>
            <div class="p-2">
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
                            v-for="(participant, index) in participants"
                            :key="participant.id"
                            :participant="participant"
                            :active-turn="encounter.currentTurnIndex === index"
                            @edit="console.log('edit participant card')"
                            @remove="emits('remove-participant', participant)"
                            @add-condition="onAddCondition"
                            @remove-condition="onRemoveCondition"
                            @edit-condition="onEditCondition"
                        />
                    </TransitionGroup>
                </VueDraggable>
                <div class="flex items-center justify-between mt-4">
                    <button class="btn btn-accent" @click="prevTurn">Prev Turn</button>
                    <h1 class="text-xl font-semibold">Round {{ encounterRef.roundNumber }}</h1>
                    <button class="btn btn-accent" @click="nextTurn">Next Turn</button>
                </div>
            </div>
        </div>
    </div>
</template>

<!--suppress CssUnusedSymbol -->
<style>
.slide-move {
    transition: all 0.5s ease;
}
</style>
