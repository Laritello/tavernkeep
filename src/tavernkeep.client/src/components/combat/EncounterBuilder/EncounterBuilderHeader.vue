<script setup lang="ts">
import { storeToRefs } from 'pinia';

import InitiativeTracker from '@/components/combat/EncounterBuilder/InitiativeTracker/InitiativeTracker.vue';
import TabMenu from '@/components/shared/TabMenu.vue';
import { useEncountersStore } from '@/stores/useEncountersStore.ts';

const encountersStore = useEncountersStore();
const { encounterList, selectedEncounterId } = storeToRefs(encountersStore);

let counter = encounterList.value.length;

async function createEncounter() {
    await encountersStore.createEncounter(`Encounter ${++counter}`);
}

function setActiveEncounter(encounterId: string) {
    selectedEncounterId.value = encounterId;
}

async function deleteEncounter(encounterId: string) {
    console.log(encounterId);
    await encountersStore.deleteEncounter(encounterId);
}
</script>

<template>
    <div class="navbar">
        <div class="flex-none lg:hidden">
            <button class="btn btn-square btn-ghost">
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    class="inline-block h-5 w-5 stroke-current"
                >
                    <path
                        stroke-linecap="round"
                        stroke-linejoin="round"
                        stroke-width="2"
                        d="M4 6h16M4 12h16M4 18h16"
                    ></path>
                </svg>
            </button>
        </div>
        <div class="flex-none">
            <RouterLink to="/" class="btn btn-ghost text-xl">Tavernkeep</RouterLink>
        </div>
        <div class="grow gap-2">
            <TabMenu
                :tabs="encounterList.map((e) => ({ id: e.id, label: e.name, encounter: e }))"
                :use-default-slot="true"
                :show-close-button="true"
                tab-max-width="9rem"
                teleport-target="#encounter-tab-content"
                variant="bordered"
                size="md"
                @tab-selected="setActiveEncounter"
                @close="deleteEncounter"
            >
                <template #default="{ props }">
                    <InitiativeTracker
                        :encounter="props.tab.encounter"
                        @participants-updated="encountersStore.updateOrder(props.tab.id, $event)"
                        @remove-participant="encountersStore.removeParticipant(props.tab.id, $event)"
                        @next-turn="console.log('Next turn')"
                        @prev-turn="console.log('Previous turn')"
                    />
                </template>
            </TabMenu>
        </div>
    </div>
</template>
