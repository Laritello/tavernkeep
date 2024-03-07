<template>
    <div class="bg-base-300 rounded p-2">
        <div class="text-center">{{ ability.type }}</div>
        <div class="text-center rounded border">{{ ability.score }}</div>
    </div>
</template>
<script setup lang="ts">
import type { Ability } from '@/contracts/character/Ability';

defineProps<{
    ability: Ability;
}>();
</script>
