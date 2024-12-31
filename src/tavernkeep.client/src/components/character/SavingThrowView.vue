<template>
    <div class="box-border flex flex-col m-2 p-2 border-2 rounded-lg red">
        <p class="text-3xl font-extrabold text-center select-none">{{ savingThrow.bonus }}</p>
        <p class="text-sm font-light text-center select-none">{{ savingThrow.type }}</p>
        <ProficiencyComponent class="place-self-center" :proficiency="savingThrow.proficiency"/>
    </div>
</template>
<script setup lang="ts">
import type { SavingThrow } from '@/contracts/character';
import ProficiencyComponent from './ProficiencyComponent.vue';

const { savingThrow } = defineProps<{ savingThrow: SavingThrow }>();
</script>

<style scoped>
</style>
