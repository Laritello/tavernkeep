import { defineStore, acceptHMRUpdate } from 'pinia';
import { ref, computed, reactive } from 'vue';

import EncounterHub from '@/api/hubs/EncounterHub.ts';
import type { Encounter } from '@/contracts/encounter/Encounter.ts';
import { ApiClientFactory } from '@/factories/ApiClientFactory.ts';

export const useEncountersStore = defineStore('encounters', () => {
    const api = ApiClientFactory.createApiClient();

    // region State
    const state = reactive({} as Record<string, Encounter>);
    const currentEncounterId = ref<string | null>(null);
    // const bestiary = ref<Participant[]>([]);
    // endregion

    // region SignalR
    EncounterHub.connection.on('OnEncounterCreated', (encounter: Encounter) => {
        state[encounter.id] = encounter;
    });

    EncounterHub.connection.on('OnEncounterUpdated', (encounter: Encounter) => {
        Object.assign(state[encounter.id], encounter);
    });

    EncounterHub.connection.on('OnEncounterDeleted', (encounterId: string) => {
        delete state[encounterId];
    });

    EncounterHub.connection.on('OnEncounterLaunched', (encounterId: string) => {
        console.log('Encounter', encounterId, 'started');
    });
    // endregion

    // region Getters
    const currentEncounter = computed(() => (!!currentEncounterId.value ? state[currentEncounterId.value] : null));
    const encounterList = computed(() => Object.values(state));
    // endregion

    // region Actions
    async function createEncounter(name = 'Encounter') {
        const encounter = await api.createEncounter(name);
        state[encounter.id] = encounter;
        currentEncounterId.value = encounter.id;
        return encounter;
    }

    async function deleteEncounter(encounterId: string) {
        await api.deleteEncounter(encounterId);
    }

    function switchEncounter(encounterId: string) {
        currentEncounterId.value = encounterId;
    }

    async function fetch() {
        const encounters = await api.getEncounters();
        Object.assign(state, encounters);
    }

    // endregion

    return {
        encounters: state,
        currentEncounterId,

        currentEncounter,
        encounterList,

        createEncounter,
        deleteEncounter,
        switchEncounter,

        fetch,
    };
});

if (import.meta.hot) {
    import.meta.hot.accept(acceptHMRUpdate(useEncountersStore, import.meta.hot));
}
