import { defineStore, acceptHMRUpdate } from 'pinia';
import { computed, reactive, ref } from 'vue';

import EncounterHub from '@/api/hubs/EncounterHub.ts';
import type { Encounter } from '@/contracts/encounter/Encounter.ts';
import type { Participant } from '@/contracts/encounter/Participant.ts';
import { ApiClientFactory } from '@/factories/ApiClientFactory.ts';

export const useEncountersStore = defineStore('encounters', () => {
    const api = ApiClientFactory.createApiClient();

    // region State
    const state = reactive({} as Record<string, Encounter>);
    const selectedEncounterId = ref<string | null>(null);
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
    const encounterList = computed(() => Object.values(state));
    // endregion

    // region Actions
    async function createEncounter(name = 'Encounter') {
        const encounter = await api.createEncounter(name);
        state[encounter.id] = encounter;
        return encounter;
    }

    async function deleteEncounter(encounterId: string) {
        await api.deleteEncounter(encounterId);
    }

    async function addParticipant(encounterId: string, participant: Pick<Participant, 'type' | 'entityId'>) {
        await api.addEncounterParticipant(encounterId, participant);
    }

    async function removeParticipant(encounterId: string, participant: Participant) {
        await api.removeEncounterParticipant(encounterId, participant);
    }

    async function updateOrder(encounterId: string, newOrder: Participant[]) {
        await api.updateEncounterParticipantsOrder(
            encounterId,
            newOrder.map((p) => p.id)
        );
    }

    async function fetch() {
        const encounters = await api.getEncounters();
        Object.assign(state, encounters);
    }
    // endregion

    return {
        encounters: state,
        selectedEncounterId,
        encounterList,

        createEncounter,
        deleteEncounter,

        addParticipant,
        removeParticipant,

        updateOrder,

        fetch,
    };
});

if (import.meta.hot) {
    import.meta.hot.accept(acceptHMRUpdate(useEncountersStore, import.meta.hot));
}
