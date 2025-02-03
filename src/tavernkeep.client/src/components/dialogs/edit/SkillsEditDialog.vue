<script setup lang="ts">
import { useI18n } from 'vue-i18n';

import ProficiencyEditDialog from '@/components/dialogs/edit/ProficiencyEditDialog.vue';
import { type DialogResultCallback } from '@/composables/useModal';
import type { Skill } from '@/contracts/character';
import { Proficiency } from '@/contracts/enums';

const { t } = useI18n();

type ReturnType = Record<string, Proficiency>;
const { closeModal, skills } = defineProps<{
    skills: Skill[];
    closeModal: DialogResultCallback<ReturnType>;
}>();

const convertedSkills = Object.values(skills).map((item) => ({
    name: item.name.toString(),
    proficiency: item.proficiency,
    userBonus: 0,
}));
</script>

<template>
    <ProficiencyEditDialog
        :caption="t('dialogs.skillsEdit.header')"
        locale-prefix="pf.skills."
        :items="convertedSkills"
        :close-modal="closeModal"
    />
</template>
