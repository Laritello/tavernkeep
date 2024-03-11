<template>
    <div class="w-56 h-64 flex flex-col justify-between text-center bg-base-300">
        <div class="text-xl bg-slate-400">{{ type.toUpperCase() }}</div>
        <div v-if="value !== undefined" class="text-2xl">{{ Math.round(value - 10 / 2) }}</div>
        <div class="absolute inset-auto flex justify-center">
            <div class="size-16 border rotate-45 flex flex-col justify-center align-middle">
                <div class="-rotate-45">{{ value }}</div>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import type { AbilityType } from '@/contracts/enums/AbilityType';

defineProps<{
    type: AbilityType;
}>();

const value = defineModel<number>();
</script>
