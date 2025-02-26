<script setup lang="ts">
import { computed } from 'vue';

import InitiativeTracker from '@/components/combat/EncounterBuilder/InitiativeTracker/InitiativeTracker.vue';
import TabMenu from '@/components/shared/TabMenu.vue';
import { useEncountersStore } from '@/stores/useEncountersStore.ts';

const encountersStore = useEncountersStore();
const tabs = computed(() => encountersStore.encountersList.map((e) => ({ id: e.id, label: e.name, encounter: e })));

async function createEncounter() {
    await encountersStore.createEncounter(`Encounter ${tabs.value.length + 1}`);
}

function setActiveEncounter(encounterId: string | undefined) {
    encountersStore.selectedEncounterId = encounterId;
}

async function deleteEncounter(encounterId: string) {
    await encountersStore.deleteEncounter(encounterId);
}
</script>

<template>
    <header class="navbar">
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
                :tabs="tabs"
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
                        @next-turn="encountersStore.nextTurn(props.tab.id)"
                        @prev-turn="encountersStore.previousTurn(props.tab.id)"
                        @roll-initiative="encountersStore.rollInitiative(props.tab.id, $event)"
                        @reset-initiative="encountersStore.resetInitiative(props.tab.id)"
                        @begin-encounter="encountersStore.beginEncounter(props.tab.id)"
                        @end-encounter="encountersStore.endEncounter(props.tab.id)"
                    />
                </template>
            </TabMenu>
            <button class="btn btn-circle btn-sm btn-ghost" @click="createEncounter">
                <span class="mdi mdi-plus"></span>
            </button>
        </div>
    </header>
</template>
