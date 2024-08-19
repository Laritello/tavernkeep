<template>
    <div class="grid grid-cols-5 gap-4">
        <div class="col-start-1">{{ ability.type }}</div>
        <div class="col-start-3">{{ ability.modifier }}</div>
        <div class="col-start-4">{{ ability.score }}</div>
        <button class="btn btn-xs w-10 col-start-5" @click="$emit('edit')">Edit</button>
    </div>
</template>
<script setup lang="ts">
import type { Ability } from '@/contracts/character';
const { ability } = defineProps<{ ability: Ability }>();
</script>
