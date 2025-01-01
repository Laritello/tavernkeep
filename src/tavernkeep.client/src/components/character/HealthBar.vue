<script setup lang="ts">
import type { Health } from '@/contracts/character';

const { health } = defineProps<{ health: Health }>();
</script>

<template>
    <div v-bind:class="health.temporary > 0 ? 'w-full bg-neutral rounded-r-xl border-2 border-sky-500' : 'w-full bg-neutral rounded-r-xl'">
        <!-- Current health -->
        <div class="bg-red-700 h-full rounded-xl" :style="{ width: `${(health.current / health.max) * 100}%` }"></div>
        <!-- Text label -->
        <div class="absolute inset-0 left-8 font-bold text-white flex flex-row text-xs self-center">
            <p>{{ health.current }} / {{ health.max }}</p>
            <p v-if="health.temporary > 0" style="white-space: pre-wrap;">{{ ` + ` }}</p>
            <p v-if="health.temporary > 0">{{ `${health.temporary}` }}</p>
        </div>
    </div>
</template>

<style scoped></style>
