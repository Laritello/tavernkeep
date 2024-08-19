<template>
    <div class="grid grid-cols-3 gap-4">
        <div class="col-start-1">{{ skill.type }}</div>
        <div class="col-start-2">{{ skill.proficiency }}</div>
        <button class="col-start-3 btn btn-xs w-10" @click="$emit('edit')">Edit</button>
    </div>
</template>

<script lang="ts" setup>
import type { Skill } from '@/contracts/character';

const { skill } = defineProps<{ skill: Skill }>();
</script>
