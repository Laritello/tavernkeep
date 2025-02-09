import { defineStore, acceptHMRUpdate } from 'pinia';
import { ref, computed } from 'vue';

import { Encounter } from '@/entities/Encounter.ts';
import type { EncounterConfig, Participant, SerializedEncounter } from '@/entities/Encounter.ts';
import { useCharacters } from '@/stores/characters.ts';

export const useEncountersStore = defineStore('encounters', () => {
    // State
    const encounters = ref<Record<string, Encounter>>({});
    const currentEncounterId = ref<string | null>(null);
    const bestiary = ref<Participant[]>([]);
    const characters = computed<Participant[]>(() => {
        const charactersStore = useCharacters();
        return Object.values(charactersStore.all).map((character) => {
            const id = character.id;
            const name = character.name;
            return {
                initiative: null,
                type: 'player',
                id,
                name,
                character,
            };
        });
    });

    // Getters
    const currentEncounter = computed(() => encounters.value[currentEncounterId.value!] ?? null);
    const encounterList = computed(() => Object.values(encounters.value));

    // Actions
    function createNewEncounter(config?: Partial<EncounterConfig>) {
        // TODO: Do it on server
        const encounter = new Encounter(config);
        encounters.value[encounter.id] = encounter;
        currentEncounterId.value = encounter.id;
        return encounter;
    }

    function importEncounter(data: SerializedEncounter) {
        const encounter = Encounter.deserialize(data);
        encounters.value[encounter.id] = encounter;
        return encounter;
    }

    function exportEncounter(encounterId: string) {
        const encounter = encounters.value[encounterId];
        return encounter?.serialize();
    }

    function deleteEncounter(encounterId: string) {
        // TODO: Do it on server
        delete encounters.value[encounterId];
        if (currentEncounterId.value === encounterId) {
            currentEncounterId.value = encounterList.value[0]?.id || null;
        }
    }

    function switchEncounter(encounterId: string) {
        currentEncounterId.value = encounterId;
    }

    return {
        encounters,
        currentEncounterId,
        characters,
        bestiary,

        currentEncounter,
        encounterList,

        createNewEncounter,
        importEncounter,
        exportEncounter,
        switchEncounter,
        deleteEncounter,
    };
});

if (import.meta.hot) {
    import.meta.hot.accept(acceptHMRUpdate(useEncountersStore, import.meta.hot));
}
