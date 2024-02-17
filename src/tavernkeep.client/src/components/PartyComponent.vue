<template>
    <v-sheet class="mx-auto pa-2">
        <CharacterComponent
            v-for="character in charactersStore.characters"
            :key="character.id"
            :character="character"
        >
        </CharacterComponent>
    </v-sheet>
</template>

<script setup lang="ts">
import CharacterComponent from './CharacterComponent.vue';
import { useCharactersStore } from '@/stores/characters.store';

const charactersStore = useCharactersStore();
</script>
