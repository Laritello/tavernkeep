<script setup lang="ts">
import { storeToRefs } from 'pinia';

import { useEncountersStore } from '@/stores/encountersStore.ts';

const encountersStore = useEncountersStore();
const { encounterList, currentEncounterId } = storeToRefs(encountersStore);

let counter = 0;

function createEncounter() {
    encountersStore.createNewEncounter({ name: `Encounter ${++counter}` });
}

function setActiveEncounter(encounterId: string) {
    encountersStore.switchEncounter(encounterId);
}

function deleteEncounter(encounterId: string) {
    encountersStore.deleteEncounter(encounterId);
}
</script>

<template>
    <div class="navbar sticky top-0 bg-base-100">
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
            <ul
                v-if="encounterList.length > 0"
                class="menu menu-xs menu-horizontal flex-nowrap gap-2 bg-base-200 rounded-box overflow-auto scroll-p-0 scroll-m-0"
            >
                <li v-for="encounter in encounterList" :key="encounter.id" class="flex flex-row flex-nowrap">
                    <span
                        :class="{ active: encounter.id === currentEncounterId }"
                        @click="setActiveEncounter(encounter.id)"
                    >
                        {{ encounter.name }}
                    </span>
                    <span class="btn btn-xs btn-circle mdi mdi-close" @click="deleteEncounter(encounter.id)"></span>
                </li>
            </ul>
            <button class="btn btn-circle btn-xs" @click="createEncounter">
                <span class="mdi mdi-plus"></span>
            </button>
        </div>
    </div>
</template>
