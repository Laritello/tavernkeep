<template>
    <div class="bg-base-100 border-2 border-base-300 rounded-xl flex flex-col p-2 w-full lg:max-w-md">
        <div class="flex flex-row justify-center items-center">
            <h2 class="mr-1 text-lg font-semibold select-none">{{ t('sections.skills') }}</h2>
            <button class="btn btn-sm btn-circle btn-ghost" @click="$router.push('character/skills/edit')">
                <GearIcon />
            </button>
        </div>

        <div class="flex flex-col">
            <SkillsWidgetItem
                v-for="skill in skills"
                :key="skill.name"
                :skill="skill"
                class="skill-item"
                @pin="(value) => $emit('pin', value)"
                @roll="(value) => $emit('roll', value.name)"
            />
        </div>
    </div>
</template>

<script setup lang="ts">
import { useI18n } from 'vue-i18n';

import GearIcon from '@/components/icons/GearIcon.vue';
import type { Skill } from '@/contracts/character';

import SkillsWidgetItem from './SkillsWidgetItem.vue';
import type { SkillEditDto } from '@/contracts/dtos';

const { t } = useI18n();

const { skills } = defineProps<{ skills: Skill[] }>();

defineEmits<{
    changed: [value: Record<string, SkillEditDto>];
    roll: [value: string];
    pin: [value: Skill]
}>();
</script>

<style scoped>
.skill-item:last-child {
    border: none;
}
</style>
