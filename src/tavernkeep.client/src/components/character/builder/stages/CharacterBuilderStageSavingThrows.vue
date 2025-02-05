<template>
    <ProficiencyListEdit v-model="template.savingThrows" locale-prefix="pf.savingThrows." />
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia';

import { useCharacterBuilderStore } from '@/components/character/builder/stores/characterBuilderStore.ts';
import ProficiencyListEdit from '@/components/character/shared/ProficiencyListEdit/ProficiencyListEdit.vue';

const characterBuilderStore = useCharacterBuilderStore();
const { template } = storeToRefs(characterBuilderStore);
</script>
