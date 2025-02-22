<script setup lang="ts">
import type { Health } from '@/contracts/character';

const {
    health,
    width = '100%',
    height = '24px',
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
        class="flex bg-neutral rounded-[10px] relative"
        :class="{ 'border-2 border-sky-500': health.temporary > 0 && !hidden }"
        :style="{ width, height }"
    >
        <!-- Current health -->
        <div
            class="bg-red-700 rounded-[8px] h-full"
            :style="{ width: `${(health.current / health.max) * 100}%` }"
        ></div>
        <!-- Text label -->
        <p class="absolute inset-0 font-bold text-white text-xs text-center">
            <span v-if="!hidden" class="align-middle">{{ health.current }} / {{ health.max }}</span>
            <span v-else class="align-middle">{{ Math.round((health.current / health.max) * 100) }}%</span>
            <template v-if="health.temporary > 0 && !hidden">
                <span class="align-middle" style="white-space: pre-wrap">{{ ` + ` }}</span>
                <span class="align-middle">{{ `${health.temporary}` }}</span>
            </template>
        </p>
    </div>
</template>

<style scoped></style>
