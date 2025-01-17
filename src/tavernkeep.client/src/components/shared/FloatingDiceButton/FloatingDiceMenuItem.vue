<script setup lang="ts">
import { ref, defineAsyncComponent } from 'vue';

const { dice } = defineProps<{
    dice: 'd4' | 'd6' | 'd8' | 'd10' | 'd12' | 'd20';
}>();

const diceIcon = defineAsyncComponent(() => {
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
function reset() {
    count.value = 0;
}

defineExpose({ count, dice, reset });
</script>

<template>
    <div class="flex flex-col items-center">
        <div class="indicator">
            <span
                class="indicator-item indicator-bottom indicator-center badge badge-sm badge-warning"
                :class="{ invisible: count == 0 }"
            >
                {{ count }}
            </span>
            <button @click="count++" @contextmenu.prevent="count = 0" class="btn btn-md btn-success btn-circle">
                <Component :is="diceIcon" class="fill-current size-8" />
            </button>
        </div>
    </div>
</template>

<style scoped></style>
