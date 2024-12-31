<template>
    <div class="box-border flex flex-col m-2 p-2 border-2 rounded-lg red">
        <p class="text-3xl font-extrabold text-center">{{ ability.modifier }}</p>
        <p class="text-sm font-light text-center">{{ ability.type }}</p>
    </div>
</template>
<script setup lang="ts">
import type { Ability } from '@/contracts/character';
const { ability } = defineProps<{ ability: Ability }>();
</script>

<style>
.align-right {
    display: flex;
    justify-content: right;
    align-items: flex-end;
}
</style>
