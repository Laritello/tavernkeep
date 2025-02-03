<template>
    <div class="tooltip" :data-tip="die">
        <component :is="dice.get(die)" v-if="dice.has(die)" class="dice" />
        <div v-else class="bg-slate-500 outline outline-1 size-6 m-2 rounded pt-[2px] text-center">?</div>
        <div v-if="value !== undefined" class="text-center text-sm">{{ value }}</div>
    </div>
</template>
<script setup lang="ts">
import d4 from '@/assets/dice/d4-grey.svg';
import d6 from '@/assets/dice/d6-grey.svg';
import d8 from '@/assets/dice/d8-grey.svg';
import d10 from '@/assets/dice/d10-grey.svg';
import d12 from '@/assets/dice/d12-grey.svg';
import d20 from '@/assets/dice/d20-grey.svg';

const dice = new Map<string, string>([
    ['d20', d20],
    ['d12', d12],
    ['d10', d10],
    ['d8', d8],
    ['d6', d6],
    ['d4', d4],
]);

defineProps<{
    die: string;
    value?: number;
}>();
</script>
<style scoped>
.text-shadow {
    text-shadow: #000000 0px 0 5px;
}

.dice {
    aspect-ratio: 1;
    fill: currentColor;
}
</style>
