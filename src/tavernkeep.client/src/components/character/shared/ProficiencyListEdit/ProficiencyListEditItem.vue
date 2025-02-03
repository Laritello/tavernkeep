<script setup lang="ts">
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';

import ProficiencyComponent from '@/components/character/ProficiencyComponent.vue';
import type { BaseSkill } from '@/contracts/character';
import { Proficiency, SkillType } from '@/contracts/enums';

const { t } = useI18n();

const { item, localePrefix } = defineProps<{
    item: BaseSkill;
    localePrefix: string;
}>();

const emits = defineEmits<{
    updated: [value: BaseSkill];
}>();

const proficiencies = [
    Proficiency.Untrained,
    Proficiency.Trained,
    Proficiency.Expert,
    Proficiency.Master,
    Proficiency.Legendary,
];
let proficiencyIndex = proficiencies.indexOf(item.proficiency);
const currentProficiency = ref(item.proficiency);

function increaseProficiency() {
    if (proficiencyIndex > 3) return;

    currentProficiency.value = proficiencies[++proficiencyIndex];
    emits('updated', { ...item, proficiency: currentProficiency.value });
}

function decreaseProficiency() {
    if (proficiencyIndex < 1) return;
    currentProficiency.value = proficiencies[--proficiencyIndex];
    emits('updated', { ...item, proficiency: currentProficiency.value });
}
</script>

<template>
    <div class="flex flex-row items-center p-1 gap-x-1 border-b-2 border-base-300">
        <p v-if="item.type === SkillType.Lore" class="grow select-none">
            {{ t('widgets.skills.lore') }}: {{ item.name }}
        </p>
        <p v-else-if="item.type === SkillType.Custom" class="grow select-none">{{ item.name }}</p>
        <p v-else class="grow select-none">{{ t(localePrefix + item.name.toLowerCase()) }}</p>

        <span class="btn btn-sm btn-circle btn-ghost" @click="decreaseProficiency()">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" viewBox="0 -960 960 960" fill="currentColor">
                <path d="M560-240 320-480l240-240 56 56-184 184 184 184-56 56Z" />
            </svg>
        </span>
        <ProficiencyComponent :proficiency="currentProficiency" />
        <span class="btn btn-sm btn-circle btn-ghost" @click="increaseProficiency()">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" viewBox="0 -960 960 960" fill="currentColor">
                <path d="M504-480 320-664l56-56 240 240-240 240-56-56 184-184Z" />
            </svg>
        </span>
    </div>
</template>

<style scoped></style>
