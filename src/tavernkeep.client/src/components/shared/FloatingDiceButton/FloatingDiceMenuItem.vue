<script setup lang="ts">
import { ref, defineAsyncComponent } from 'vue';
import { breakpointsTailwind, useBreakpoints } from '@vueuse/core';

const breakpoints = useBreakpoints(breakpointsTailwind);
const isDesktop = breakpoints.greater('lg');

const { dice } = defineProps<{
    dice: 'd4' | 'd6' | 'd8' | 'd10' | 'd12' | 'd20';
}>();

const diceIconComponent = defineAsyncComponent(() => {
    switch (dice) {
        case 'd4':
            return import('@/assets/dice/d4-grey.svg');
        case 'd6':
            return import('@/assets/dice/d6-grey.svg');
        case 'd8':
            return import('@/assets/dice/d8-grey.svg');
        case 'd10':
            return import('@/assets/dice/d10-grey.svg');
        case 'd12':
            return import('@/assets/dice/d12-grey.svg');
        case 'd20':
            return import('@/assets/dice/d20-grey.svg');
    }
});

const count = ref(0);

function onClickOrTap() {
    count.value++;
}

function onRightClick() {
    if (count.value == 0) return;
    count.value--;
}

function onLongTap() {
    if (isDesktop.value) {
        return;
    }

    reset();
}

function reset() {
    count.value = 0;
}

defineExpose({ count, dice, reset });
</script>

<template>
    <div class="flex flex-col items-center">
        <div class="indicator">
            <span
                class="indicator-item indicator-top indicator-start lg:indicator-center badge badge-sm badge-warning"
                :class="{ invisible: count == 0 }"
            >
                {{ count }}
            </span>
            <button
                @click="onClickOrTap"
                @click.right.prevent="onRightClick"
                @contextmenu.prevent="onLongTap"
                class="btn btn-md btn-success btn-circle"
            >
                <Component :is="diceIconComponent" class="fill-current size-8" />
            </button>
        </div>
    </div>
</template>

<style scoped></style>
