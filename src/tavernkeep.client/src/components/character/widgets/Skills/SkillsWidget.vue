<template>
    <div class="bg-base-100 border-2 border-base-300 rounded-xl border-inherit flex flex-col p-2 w-full lg:max-w-md">
        <div class="flex flex-row justify-center items-center">
            <h2 class="mr-1 text-lg font-semibold select-none">{{ t('sections.skills') }}</h2>
            <button class="btn btn-sm btn-circle btn-ghost" @click="showEditSkillsDialog()">
                <GearIcon />
            </button>
        </div>

        <div class="flex flex-col">
            <SkillsWidgetItem v-for="skill in skills" :skill="skill" :key="skill.name" class="skill-item"
                @roll="(value) => $emit('roll', value.name)" />
        </div>
    </div>
</template>

<script setup lang="ts">
import type { Skill } from '@/contracts/character';
import SkillsWidgetItem from './SkillsWidgetItem.vue';
import SkillsEditDialog from '@/components/dialogs/edit/SkillsEditDialog.vue';
import { useModal } from '@/composables/useModal';
import { Proficiency } from '@/contracts/enums';
import { useI18n } from 'vue-i18n';
import GearIcon from '@/components/icons/GearIcon.vue';

const { t } = useI18n();

const { skills } = defineProps<{ skills: Skill[] }>();

const emits = defineEmits<{
    changed: [value: Record<string, Proficiency>],
    roll: [value: string]
}>();

async function showEditSkillsDialog() {
    const modal = useModal();
    const result = await modal.show(SkillsEditDialog, {
        skills,
    });

    if (result.action === 'result') {
        emits('changed', result.payload);
    }
}
</script>

<style scoped>
.skill-item:last-child {
    border:none;
}
</style>
