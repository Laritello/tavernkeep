<template>
    <div class="grid grid-cols-3 gap-4">
        <div class="col-start-1">{{ ability.type }}</div>
        <div class="col-start-2">{{ ability.score }}/{{ ability.modifier }}</div>
        <button class="btn btn-xs w-10 col-start-3" @click="$emit('edit')">Edit</button>
    </div>
</template>
<script setup lang="ts">
import type { Ability } from '@/contracts/character';
const { ability } = defineProps<{ ability: Ability }>();
</script>
