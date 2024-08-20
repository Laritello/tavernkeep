<template>
    <div class="card card-body bg-neutral text-neutral-content">
        <h1 class="card-title">Perception</h1>
        <div class="grid grid-cols-4 gap-4">
            <div class="col-start-1">Bonus</div>
            <span class="col-start-2 mx-auto">{{ perception.bonus }}</span>
            <div class="col-start-3">{{ perception.proficiency }}</div>
            <div class="align-right">
                <button class="btn btn-xs w-10" @click="$emit('edit', perception)">Edit</button>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { Perception } from '@/contracts/character';

defineProps<{
    perception: Perception;
}>();
</script>
<style>
.align-right {
    display: flex;
    justify-content: right;
    align-items: flex-end;
}
</style>
