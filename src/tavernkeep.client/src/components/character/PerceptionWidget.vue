<template>
    <div class="flex items-center justify-center relative">
        <svg viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path
                d="M19.9604 11.4802C19.9604 13.8094 19.0227 15.9176 17.5019 17.4512C16.9332 18.0247 16.2834 18.5173 15.5716 18.9102C14.3594 19.5793 12.9658 19.9604 11.4802 19.9604C6.79672 19.9604 3 16.1637 3 11.4802C3 6.79672 6.79672 3 11.4802 3C16.1637 3 19.9604 6.79672 19.9604 11.4802Z"
                stroke="#2D3748"
                fill="#4A5568"
                stroke-width="2"
            />
            <path d="M18.1553 18.1553L21.8871 21.8871" stroke="#2D3748" stroke-width="2" stroke-linecap="round" />
        </svg>
        <div
            class="absolute text-xl text-neutral-content font-bold top-[47%] left-[45%] -translate-x-1/2 -translate-y-1/2"
        >
            {{ perception.bonus }}
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
