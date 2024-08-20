<template>
    <div class="grid grid-cols-3 gap-4">
        <div>{{ ability.type }}</div>
        <div>{{ ability.score }}/{{ ability.modifier }}</div>
        <div class="align-right">
            <button class="btn btn-xs w-10" @click="$emit('edit')">Edit</button>
        </div>
    </div>
</template>
<script setup lang="ts">
import type { Ability } from '@/contracts/character';
const { ability } = defineProps<{ ability: Ability }>();
</script>

<style>
.align-right {
    display: flex;
    justify-content: right;
    align-items: flex-end;
}
</style>
