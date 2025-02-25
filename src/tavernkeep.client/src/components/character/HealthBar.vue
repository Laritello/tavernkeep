<script setup lang="ts">
import type { Health } from '@/contracts/character';

const {
    health,
    width = '100%',
    height = '1.5rem',
} = defineProps<{
    health: Health;
    width?: string;
    height?: string;
}>();
</script>

<template>
    <div
        class="flex bg-neutral rounded-full relative cursor-default hover:border-2 hover:border-secondary"
        :class="{ 'border-2 border-sky-500': health.temporary > 0 }"
        :style="{ width, height }"
    >
        <!-- Current health -->
        <div class="bg-red-700 rounded-full h-full" :style="{ width: `${(health.current / health.max) * 100}%` }"></div>
        <!-- Text label -->
        <div class="flex absolute inset-0 font-bold text-xs text-white text-center justify-center items-center">
            <span>{{ health.current }} / {{ health.max }}</span>
            <template v-if="health.temporary > 0">
                <span style="white-space: pre-wrap">{{ ` + ` }}</span>
                <span>{{ `${health.temporary}` }}</span>
            </template>
        </div>
    </div>
</template>

<style scoped></style>
