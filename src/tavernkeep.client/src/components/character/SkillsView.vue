<template>
    <div class="card bg-neutral text-neutral-content w-96">
        <div class="flex flex-col card-body">
            <SkillView v-for="skill in skills" :skill="skill" :key="skill.type" @edit="$emit('edit', skill)" />
        </div>
    </div>
</template>

<script setup lang="ts">
import type { Skill } from '@/contracts/character';
import SkillView from './SkillView.vue';

const { skills } = defineProps<{ skills: Record<string, Skill> }>();
</script>
