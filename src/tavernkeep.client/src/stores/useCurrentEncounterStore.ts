import { acceptHMRUpdate, defineStore } from 'pinia';
import { computed } from 'vue';

import type { Participant } from '@/contracts/encounter/Participant.ts';
import { ApiClientFactory } from '@/factories/ApiClientFactory.ts';
import { useEncountersStore } from '@/stores/useEncountersStore.ts';

export const useCurrentEncounterStore = defineStore('current-encounter', () => {
    const api = ApiClientFactory.createApiClient();
    const encountersStore = useEncountersStore();

    const participants = computed({
        get: () => encountersStore.currentEncounter?.participants ?? [],
        set: (value) => {
            if (!encountersStore.currentEncounter) {
                throw new Error('Impossible to change participants when "currentEncounter" not selected');
            }

            encountersStore.currentEncounter.participants = value;
        },
    });
    const isActive = computed(() => !!encountersStore.currentEncounterId);

    async function addParticipant(participant: Pick<Participant, 'type' | 'entityId'>) {
        if (!encountersStore.currentEncounterId) {
            console.warn('Encounter not selected');
            return;
        }

        await api.addEncounterParticipant(encountersStore.currentEncounterId, participant);
    }

    async function removeParticipant(participant: Participant) {
        if (!encountersStore.currentEncounterId) {
            console.warn('Encounter not selected');
            return;
        }

        await api.removeEncounterParticipant(encountersStore.currentEncounterId, participant);
    }

    async function updateOrder() {
        if (!encountersStore.currentEncounterId) {
            console.warn('Encounter not selected');
            return;
        }

        await api.updateEncounterParticipantsOrder(
            encountersStore.currentEncounterId,
            participants.value.map((p) => p.id)
        );
    }

    return { isActive, participants, addParticipant, removeParticipant, updateOrder };
});

if (import.meta.hot) {
    import.meta.hot.accept(acceptHMRUpdate(useCurrentEncounterStore, import.meta.hot));
}
