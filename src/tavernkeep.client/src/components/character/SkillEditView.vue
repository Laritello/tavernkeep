<template>
    <div class="flex flex-row items-center p-1 border-b-2">
        <p class="grow select-none">{{ skill.type }}</p>
        <span class="btn btn-sm btn-circle btn-ghost" @click="decreaseProficiency()">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" viewBox="0 -960 960 960" fill="currentColor"><path
                d="M560-240 320-480l240-240 56 56-184 184 184 184-56 56Z" /></svg>
        </span>
        <ProficiencyComponent :proficiency="currentProficiency" />
        <span class="btn btn-sm btn-circle btn-ghost " @click="increaseProficiency()">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" viewBox="0 -960 960 960"  fill="currentColor"><path
                d="M504-480 320-664l56-56 240 240-240 240-56-56 184-184Z" /></svg>
        </span>
    </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import type { Skill } from '@/contracts/character';
import ProficiencyComponent from './ProficiencyComponent.vue';
import { Proficiency } from '@/contracts/enums';

const { skill } = defineProps<{
    skill: Skill;
}>();

const emits = defineEmits<{
    onChange: [value: Skill]
}>();

const proficiencies = [ 
    Proficiency.Untrained, 
    Proficiency.Trained, 
    Proficiency.Expert, 
    Proficiency.Master, 
    Proficiency.Legendary 
];
let proficiencyIndex = proficiencies.indexOf(skill.proficiency);
const currentProficiency = ref(skill.proficiency);

function increaseProficiency() {
    if (proficiencyIndex > 3) return;

    currentProficiency.value = proficiencies[++proficiencyIndex];
    emits('onChange', { ...skill, proficiency: currentProficiency.value });
}

function decreaseProficiency() {
    if (proficiencyIndex < 1) return;
    currentProficiency.value = proficiencies[--proficiencyIndex];
    emits('onChange', { ...skill, proficiency: currentProficiency.value });
}
</script>
<style scoped></style>
