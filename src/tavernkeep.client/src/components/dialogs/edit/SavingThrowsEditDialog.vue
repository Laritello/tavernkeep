<script setup lang="ts">
import { useI18n } from 'vue-i18n';

import ProficiencyEditDialog from '@/components/dialogs/edit/ProficiencyEditDialog.vue';
import { type DialogResultCallback } from '@/composables/useModal';
import type { SavingThrow } from '@/contracts/character';
import { Proficiency } from '@/contracts/enums';

const { t } = useI18n();

type ReturnType = Record<string, Proficiency>;
const { closeModal, savingThrows } = defineProps<{
    savingThrows: SavingThrow[];
    closeModal: DialogResultCallback<ReturnType>;
}>();

const convertedSavingThrows = Object.values(savingThrows).map((s) => ({
    name: s.name.toString(),
    proficiency: s.proficiency,
    userBonus: 0,
}));
</script>

<template>
    <ProficiencyEditDialog
        :caption="t('dialogs.savingThrowsEdit.header')"
        localePrefix="pf.savingThrows."
        :items="convertedSavingThrows"
        :closeModal="closeModal"
    />
</template>
