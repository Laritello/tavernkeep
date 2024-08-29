<template>
    <div class="card card-body bg-neutral text-neutral-content w-auto">
        <h1 class="card-title">Skills</h1>
        <div class="flex flex-col gap-1">
            <SkillView v-for="skill in skills" :skill="skill" :key="skill.type" @edit="$emit('edit', skill)" />
        </div>
    </div>
</template>

<script setup lang="ts">
import type { Skill } from '@/contracts/character';
import SkillView from './SkillView.vue';

const { skills } = defineProps<{ skills: Record<string, Skill> }>();
</script>
