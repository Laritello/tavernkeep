<template>
    <div class="flex flex-col h-dvh">
        <EncounterBuilderHeader class="header" />
        <EncounterBuilderLeftPanel class="left-panel p-2" />
        <div class="right-panel"></div>
        <main class="content">
            <InitiativeTracker v-if="!!encountersStore.currentEncounter" class="p-4" />
        </main>
    </div>
</template>

<script setup lang="ts">
import EncounterBuilderHeader from '@/components/combat/EncounterBuilder/EncounterBuilderHeader.vue';
import EncounterBuilderLeftPanel from '@/components/combat/EncounterBuilder/EncounterBuilderLeftPanel.vue';
import InitiativeTracker from '@/components/combat/EncounterBuilder/InitiativeTracker/InitiativeTracker.vue';
import { useEncountersStore } from '@/stores/useEncountersStore.ts';

const encountersStore = useEncountersStore();
</script>

<style>
:root {
    --encounter-header-height: 4rem;
    --encounter-left-panel-width: 22rem;
    --encounter-right-panel-width: min(40rem, 80vw);
}
</style>

<style scoped>
.header {
    position: fixed;
    width: 100vw;
    height: var(--encounter-header-height);
    z-index: 3;
    @apply bg-base-100 shadow border-base-200 border-b-[1px];
}

.left-panel {
    position: fixed;
    top: var(--encounter-header-height);
    left: 0;
    height: calc(100dvh - var(--encounter-header-height));
    width: var(--encounter-left-panel-width);
    z-index: 2;
    @apply bg-base-200 shadow-lg border-base-300 border-r-[1px] hidden lg:flex;
}

.right-panel {
    position: fixed;
    top: var(--encounter-header-height);
    right: calc(var(--encounter-right-panel-width) * -1);
    height: calc(100dvh - var(--encounter-header-height));
    width: var(--encounter-right-panel-width);
    z-index: 2;
    transition: right 0.25s;
    @apply bg-base-200 shadow-lg border-base-300 border-r-[1px];
}

.right-panel-open {
    right: 0;
}

.content {
    position: fixed;
    top: var(--encounter-header-height);
    left: 0;
    width: 100vw;
    height: calc(100dvh - var(--encounter-header-height));
    z-index: 1;
    @apply bg-base-300 lg:left-[var(--encounter-left-panel-width)] lg:w-[calc(100vw-var(--encounter-left-panel-width))];
}
</style>
