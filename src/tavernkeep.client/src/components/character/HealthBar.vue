<script setup lang="ts">
import type { Health } from '@/contracts/character';

const { health } = defineProps<{ health: Health }>();
</script>

<template>
    <div
        class="flex bg-neutral rounded-[10px] w-full relative"
        :class="health.temporary > 0 ? 'border-2 border-sky-500' : ''"
    >
        <!-- Current health -->
        <div
            class="bg-red-700 rounded-[8px] h-full"
            :style="{ width: `${(health.current / health.max) * 100}%` }"
        ></div>
        <!-- Text label -->
        <p class="absolute inset-0 font-bold text-white text-xs text-center">
            <span class="align-middle">{{ health.current }} / {{ health.max }}</span>
            <span v-if="health.temporary > 0" class="align-middle" style="white-space: pre-wrap">{{ ` + ` }}</span>
            <span v-if="health.temporary > 0" class="align-middle">{{ `${health.temporary}` }}</span>
        </p>
    </div>
</template>

<style scoped></style>
