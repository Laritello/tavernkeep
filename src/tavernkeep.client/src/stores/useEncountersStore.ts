import { defineStore, acceptHMRUpdate } from 'pinia';
import { computed, reactive, ref } from 'vue';

import EncounterHub from '@/api/hubs/EncounterHub.ts';
import type { Encounter } from '@/contracts/encounter/Encounter.ts';
import { EncounterStateType } from '@/contracts/encounter/EncounterStateType.ts';
import type { Participant } from '@/contracts/encounter/Participant.ts';
import { ApiClientFactory } from '@/factories/ApiClientFactory.ts';

export const useEncountersStore = defineStore('encounters', () => {
    const api = ApiClientFactory.createApiClient();

    // region State
    const state = reactive({} as Record<string, Encounter>);
    const selectedEncounterId = ref<string>();
    // endregion

    // region Getters
    const encountersList = computed(() => Object.values(state).sort((a, b) => a.createdAt - b.createdAt));
    // endregion

    // region SignalR
    EncounterHub.connection.on('OnEncounterCreated', (encounter: Encounter) => {
        state[encounter.id] = encounter;
    });

    EncounterHub.connection.on('OnEncounterUpdated', (encounter: Encounter) => {
        console.log(encounter);
        Object.assign(state[encounter.id], encounter);
    });

    EncounterHub.connection.on('OnEncounterDeleted', (encounterId: string) => {
        delete state[encounterId];
    });

    EncounterHub.connection.on('OnEncounterLaunched', (encounterId: string) => {
        console.log('Encounter', encounterId, 'started');
    });
    // endregion

    // region Actions
    async function createEncounter(name = 'Encounter') {
        await api.createEncounter(name);
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

    async function nextTurn(encounterId: string) {
        await api.encounterNextTurn(encounterId);
    }

    async function previousTurn(encounterId: string) {
        await api.encounterPreviousTurn(encounterId);
    }

    async function rollInitiative(encounterId: string, onlyNpc: boolean) {
        await api.encounterRollInitiative(encounterId, onlyNpc);
    }

    async function resetInitiative(encounterId: string) {
        await api.encounterResetInitiative(encounterId);
    }

    async function beginEncounter(encounterId: string) {
        await api.updateEncounterState(encounterId, EncounterStateType.Active);
    }

    async function endEncounter(encounterId: string) {
        await api.updateEncounterState(encounterId, EncounterStateType.Finished);
    }

    async function fetch() {
        const encounters = await api.getEncounters();
        Object.assign(state, encounters);
    }
    // endregion

    return {
        encounters: state,
        selectedEncounterId,
        encountersList,

        createEncounter,
        deleteEncounter,
        beginEncounter,
        endEncounter,

        addParticipant,
        removeParticipant,

        nextTurn,
        previousTurn,
        rollInitiative,
        resetInitiative,

        updateOrder,

        fetch,
    };
});

if (import.meta.hot) {
    import.meta.hot.accept(acceptHMRUpdate(useEncountersStore, import.meta.hot));
}
