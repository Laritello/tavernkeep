<template>
    <div>
        <component :is="dice.get(die)" class="dice" :class="[die]" />
        <div>{{ value }}</div>
    </div>
</template>
<script setup lang="ts">
import type { Component } from 'vue';
import d20 from '@/assets/dice/d20-grey.svg?component';
import d12 from '@/assets/dice/d12-grey.svg?component';
import d10 from '@/assets/dice/d10-grey.svg?component';
import d8 from '@/assets/dice/d8-grey.svg?component';
import d6 from '@/assets/dice/d6-grey.svg?component';
import d4 from '@/assets/dice/d4-grey.svg?component';

const dice = new Map<string, Component>([
    ['d20', d20],
    ['d12', d12],
    ['d10', d10],
    ['d8', d8],
    ['d6', d6],
    ['d4', d4],
]);
defineProps<{
    die: string;
    value: number;
}>();
</script>
<style scoped>
.text-shadow {
    text-shadow: #000000 0px 0 5px;
}
.dice {
    height: 100%;
    max-width: fit-content;
}

.dice path {
    fill: #d88440;
}

.d20 path {
    fill: #d88440;
}

.d12 path {
    fill: #d88440;
}

.d10 path {
    fill: #d88440;
}

.d8 path {
    fill: #d88440;
}
.d6 path {
    fill: #d88440;
}
.d4 path {
    fill: #d88440;
}
</style>
