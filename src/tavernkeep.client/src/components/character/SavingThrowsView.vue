<template>
    <div class="card card-body bg-neutral text-neutral-content">
        <h1 class="card-title">Saving throws</h1>
        <div v-for="savingThrow in savingThrows" :key="savingThrow.type" class="grid grid-cols-4 gap-4">
            <div class="col-start-1">{{ savingThrow.type }}</div>
            <span class="col-start-2 mx-auto">{{ savingThrow.bonus }}</span>
            <div class="col-start-3">{{ savingThrow.proficiency }}</div>
            <div class="align-right">
                <button class="btn btn-xs w-10" @click="$emit('edit', savingThrow)">Edit</button>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { SavingThrow } from '@/contracts/character/SavingThrow';
import type { SavingThrowType } from '@/contracts/enums';

defineProps<{
    savingThrows: Record<SavingThrowType, SavingThrow>;
}>();
</script>
<style>
.align-right {
    display: flex;
    justify-content: right;
    align-items: flex-end;
}
</style>
