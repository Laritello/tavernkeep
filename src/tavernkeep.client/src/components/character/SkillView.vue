<template>
    <div class="grid grid-cols-4 gap-4">
        <div class="col-start-1">{{ skill.type }}</div>
        <span class="col-start-2 mx-auto">{{ skill.bonus }}</span>
        <div class="col-start-3">{{ skill.proficiency }}</div>
        <button class="col-start-4 btn btn-xs w-10 mx-auto" @click="$emit('edit')">Edit</button>
    </div>
</template>

<script lang="ts" setup>
import type { Skill } from '@/contracts/character';

const { skill } = defineProps<{ skill: Skill }>();
</script>
