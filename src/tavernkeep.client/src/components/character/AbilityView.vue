<template>
    <div class="w-56 h-64 flex flex-col justify-between text-center bg-base-300">
        <div class="text-xl bg-slate-400">{{ ability.type.toUpperCase() }}</div>
        <div class="text-2xl">{{ Math.round((ability.score - 10) / 2) }}</div>
        <div class="absolute inset-auto flex justify-center">
            <div class="size-16 border rotate-45 flex flex-col justify-center align-middle">
                <div class="-rotate-45">{{ ability.score }}</div>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import type { Ability } from '@/contracts/character';
const { ability } = defineProps<{ ability: Ability }>();
</script>
