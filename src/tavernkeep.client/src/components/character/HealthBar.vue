<script setup lang="ts">
import type { Health } from '@/contracts/character';

const { health } = defineProps<{ health: Health }>();
</script>

<template>
    <div class="w-full bg-neutral h-4 rounded relative text-xs text-neutral-content">
        <!-- Current health -->
        <div class="bg-red-800 h-4 rounded" :style="{ width: `${(health.current / health.max) * 100}%` }"></div>
        <!-- Temporary health -->
        <div
            class="bg-cyan-600 h-4 rounded absolute top-0 left-0"
            :style="{ width: `${(health.temporary / health.max) * 100}%` }"
        ></div>
        <!-- Text label -->
        <div class="absolute inset-0 flex items-center justify-center font-bold">
            {{ health.current }} / {{ health.max }}
        </div>
    </div>
</template>

<style scoped></style>
