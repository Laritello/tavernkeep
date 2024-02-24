<template>
    <PartyComponent />
</template>

<script setup lang="ts">
import PartyComponent from '@/components/PartyComponent.vue';

import { useCharactersStore } from '@/stores/characters.store';
const characterStore = useCharactersStore();

characterStore.fetchCharacters();
</script>
