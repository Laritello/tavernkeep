<script setup lang="ts">
import { type DialogResultCallback } from '@/composables/useModal';
import type { SavingThrow } from '@/contracts/character';
import { Proficiency, SavingThrowType } from '@/contracts/enums';
import ProficiencyEditDialog from '@/components/dialogs/edit/ProficiencyEditDialog.vue';

type ReturnType = Record<string, Proficiency>;
const { closeModal, savingThrows } = defineProps<{
    savingThrows: Record<SavingThrowType, SavingThrow>;
    closeModal: DialogResultCallback<ReturnType>;
}>();

const convertedSavingThrows = Object
    .values(savingThrows)
    .map(s => ({ name: s.type.toString(), proficiency: s.proficiency, userBonus: 0 }));
</script>

<template>
    <ProficiencyEditDialog caption="Edit saving throws" :items="convertedSavingThrows" :closeModal="closeModal" />
</template>
