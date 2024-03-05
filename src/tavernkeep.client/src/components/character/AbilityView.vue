<template>
    <div>
        <div>{{ ability.type }}</div>
        <div class="text-center">{{ ability.score }}</div>
    </div>
</template>
<script setup lang="ts">
import type { Ability } from '@/contracts/character/Ability';

defineProps<{
    ability: Ability;
}>();
</script>
