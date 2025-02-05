<template>
    <ProficiencyListEdit v-model="template.skills" locale-prefix="pf.skills." />
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia';

import { useCharacterBuilderStore } from '@/components/character/builder/stores/characterBuilderStore.ts';
import ProficiencyListEdit from '@/components/character/shared/ProficiencyListEdit/ProficiencyListEdit.vue';

const characterBuilderStore = useCharacterBuilderStore();
const { template } = storeToRefs(characterBuilderStore);
</script>
