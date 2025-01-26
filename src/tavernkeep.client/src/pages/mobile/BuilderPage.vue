<script setup lang="ts">
import CharacterBuilder from '@/components/character/builder/CharacterBuilder.vue';
import { useHeaderStore } from '@/stores/header';
import { onMounted } from 'vue';

const header = useHeaderStore();

onMounted(() => {
    header.setHeader("Create character");
});

function updateStage(stageName: string | undefined) {
    header.setHeader("Create character", stageName);
}

</script>

<template>
    <CharacterBuilder v-on:updated-stage="updateStage"/>
</template>

<style scoped></style>