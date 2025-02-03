<template>
    <div
        class="box-border border-base-300 flex flex-col m-2 p-1 border-2 rounded-lg red active:bg-gray-400"
        @click="emit('roll', savingThrow)"
    >
        <p class="text-3xl font-extrabold text-center select-none">{{ savingThrow.bonus }}</p>
        <p class="text-xs text-center select-none">{{ t(`pf.savingThrows.${savingThrow.name.toLowerCase()}`) }}</p>
        <ProficiencyComponent class="place-self-center" :proficiency="savingThrow.proficiency" />
    </div>
</template>
<script setup lang="ts">
import { useI18n } from 'vue-i18n';

import ProficiencyComponent from '@/components/character/ProficiencyComponent.vue';
import type { SavingThrow } from '@/contracts/character';

const { t } = useI18n();

const { savingThrow } = defineProps<{ savingThrow: SavingThrow }>();
const emit = defineEmits<{
    roll: [SavingThrow];
}>();
</script>

<style scoped></style>
