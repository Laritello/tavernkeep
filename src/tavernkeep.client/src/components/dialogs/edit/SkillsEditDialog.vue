<script setup lang="ts">
import { type DialogResultCallback } from '@/composables/useModal';
import type { Skill } from '@/contracts/character';
import { Proficiency, SkillType } from '@/contracts/enums';
import ProficiencyEditDialog from '@/components/dialogs/edit/ProficiencyEditDialog.vue';

type ReturnType = Record<string, Proficiency>;
const { closeModal, skills } = defineProps<{
    skills: Record<SkillType, Skill>;
    closeModal: DialogResultCallback<ReturnType>;
}>();

const convertedSkills = Object
    .values(skills)
    .map(item => ({ name: item.type.toString(), proficiency: item.proficiency, userBonus: 0 }));
</script>

<template>
    <ProficiencyEditDialog caption="Edit skills" :items="convertedSkills" :closeModal="closeModal" />
</template>
