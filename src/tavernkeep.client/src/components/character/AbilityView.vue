<template>
    <tr>
        <th>{{ ability.type }}</th>
        <td>{{ ability.score }}</td>
        <td>{{ ability.modifier }}</td>
    </tr>
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
