<template>
    <div class="flex flex-row items-center p-1 gap-x-2 border-b-2">
        <p class="grow">{{ skill.type }}</p>
        <ProficiencyComponent :proficiency="skill.proficiency"/>
        <p class="border-2 rounded-md w-12 text-center active:bg-gray-400" @click="emit('roll', skill)">{{ skill.bonus }}</p>
    </div>
</template>

<script lang="ts" setup>
import type { Skill } from '@/contracts/character';
import ProficiencyComponent from './ProficiencyComponent.vue';

const { skill } = defineProps<{
    skill: Skill;
}>();

const emit = defineEmits<{
    roll: [Skill];
}>();
</script>
<style></style>
