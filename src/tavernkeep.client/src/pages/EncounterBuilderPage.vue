<template>
    <div class="flex flex-col bg-base-200 h-dvh">
        <EncounterBuilderHeader class="bg-base-100 shadow border-base-200 border-b-[1px]" />
        <div class="flex flex-row grow">
            <EncounterBuilderLeftPanel
                class="min-w-72 bg-base-100 shadow border-base-200 border-r-[1px] hidden lg:flex"
            />
            <main class="container mx-auto p-4">
                <InitiativeTracker v-if="!!encountersStore.currentEncounter" class="p-4" />
            </main>
        </div>
    </div>
</template>

<script setup lang="ts">
import EncounterBuilderHeader from '@/components/combat/EncounterBuilder/EncounterBuilderHeader.vue';
import EncounterBuilderLeftPanel from '@/components/combat/EncounterBuilder/EncounterBuilderLeftPanel.vue';
import InitiativeTracker from '@/components/combat/EncounterBuilder/InitiativeTracker/InitiativeTracker.vue';
import { useEncountersStore } from '@/stores/encountersStore.ts';

const encountersStore = useEncountersStore();
</script>
