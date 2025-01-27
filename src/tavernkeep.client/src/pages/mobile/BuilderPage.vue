<script setup lang="ts">
import CharacterBuilder from '@/components/character/builder/CharacterBuilder.vue';
import type { Character } from '@/entities';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import { useHeaderStore } from '@/stores/header';
import { onMounted, ref, type Ref } from 'vue';

const header = useHeaderStore();
const api = ApiClientFactory.createApiClient();

const template: Ref<Character> = ref({} as Character);

onMounted(async () => {
    header.setHeader('Create character', 'Welcome');
    template.value = await api.getCharacterTemplate();
    console.log(template);
});

function updateStage(stageName: string | undefined) {
    header.setHeader('Create character', stageName);
}

</script>

<template>
    <CharacterBuilder v-on:updated-stage="updateStage" :character="template"/>
</template>

<style scoped></style>