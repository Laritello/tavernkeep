<template>
    <v-sheet class="mx-auto pa-2 h-full overflow-auto">
        <CharacterComponent v-for="character in charactersStore.all" :key="character.id" :character="character" />
    </v-sheet>
</template>

<script setup lang="ts">
import { useCharacters } from '@/stores/characters';

import CharacterComponent from './CharacterComponent.vue';

const charactersStore = useCharacters();
</script>
