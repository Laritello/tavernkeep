<script setup lang="ts">
import type { Health } from '@/contracts/character';

const {
    health,
    width = '100%',
    height = '1.5rem',
    hidden = false,
} = defineProps<{
    health: Health;
    width?: string;
    height?: string;
    hidden?: boolean;
}>();
</script>

<template>
    <div
        class="flex bg-neutral rounded-full relative"
        :class="{ 'border-2 border-sky-500': health.temporary > 0 && !hidden }"
        :style="{ width, height }"
    >
        <!-- Current health -->
        <div class="bg-red-700 rounded-full h-full" :style="{ width: `${(health.current / health.max) * 100}%` }"></div>
        <!-- Text label -->
        <div class="flex absolute inset-0 font-bold text-xs text-white text-center justify-center items-center">
            <span v-if="!hidden">{{ health.current }} / {{ health.max }}</span>
            <span v-else>{{ Math.round((health.current / health.max) * 100) }}%</span>
            <template v-if="health.temporary > 0 && !hidden">
                <span style="white-space: pre-wrap">{{ ` + ` }}</span>
                <span>{{ `${health.temporary}` }}</span>
            </template>
        </div>
    </div>
</template>

<style scoped></style>
