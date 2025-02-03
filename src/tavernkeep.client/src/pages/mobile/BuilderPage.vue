<script setup lang="ts">
import CharacterBuilder from '@/components/character/builder/CharacterBuilder.vue';
import type { Character } from '@/entities';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import { useCharacters } from '@/stores/characters';
import { useHeaderStore } from '@/stores/header';
import { onMounted, ref, type Ref } from 'vue';

const characters = useCharacters();
const header = useHeaderStore();
const api = ApiClientFactory.createApiClient();

const template: Ref<Character> = ref({} as Character);

onMounted(async () => {
    header.setHeader('Create character', 'Welcome');
    template.value = await api.getCharacterTemplate();
});

function updateStage(stageName: string | undefined) {
    header.setHeader('Create character', stageName);
}

function updateCharacter({ key, value }: { key: keyof Character; value: unknown }) {
    (template.value[key] as typeof value) = value;
};

function create() {
    console.log(template.value);
    characters.createCharacter(template.value);
}
</script>

<template>
    <CharacterBuilder v-on:updated-stage="updateStage" :character="template" @create="create"
        @update-character="updateCharacter" />
</template>

<style scoped></style>