<template>
    <div class="flex flex-col">
        <SkillView v-for="skill in skills" :skill="skill" :key="skill.type" />
    </div>
</template>

<script setup lang="ts">
import type { Skill } from '@/contracts/character';
import SkillView from './SkillView.vue';

const { skills } = defineProps<{ skills: Record<string, Skill> }>();
</script>
