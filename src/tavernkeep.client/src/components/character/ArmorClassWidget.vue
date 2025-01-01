<script setup lang="ts">
import type { ArmorClass } from '@/contracts/character';

defineProps<{
    armor: ArmorClass;
}>();
</script>

<template>
    <div class="flex items-center justify-center">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960" class="w-full h-full" fill="currentColor">
            <path d="M480-80q-139-35-229.5-159.5T160-516v-244l320-120 320 120v244q0 152-90.5 276.5T480-80Zm0-84q104-33 172-132t68-220v-189l-240-90-240 90v189q0 121 68 220t172 132Zm0-316Z" />
        </svg>
        <p class="absolute text-lg font-bold tracking-tighter select-none">
            {{ armor.class }}
        </p>
    </div>
</template>

<style scoped></style>