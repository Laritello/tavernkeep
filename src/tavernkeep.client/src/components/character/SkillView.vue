<template>
    <div class="grid grid-cols-4 gap-4">
        <div class="">{{ skill.type }}</div>
        <span class="mx-auto">{{ skill.bonus }}</span>
        <div class="">{{ skill.proficiency }}</div>
        <div class="align-right">
            <button class="btn btn-xs w-10" @click="$emit('edit')">Edit</button>
        </div>
    </div>
</template>

<script lang="ts" setup>
import type { Skill } from '@/contracts/character';

const { skill } = defineProps<{ skill: Skill }>();
</script>
<style>
.align-right {
    display: flex;
    justify-content: right;
    align-items: flex-end;
}
</style>
