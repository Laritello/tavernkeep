<script setup lang="ts">
import { ref, computed } from 'vue';
import { useSwipe } from '@vueuse/core';
import type { Ability } from '@/contracts/character';

const { ability, max, min, swipeSensitivity } = withDefaults(defineProps<{
    ability: Ability;
    max?: number;
    min?: number;
    swipeSensitivity?: number;
}>(), {
    max: Number.MAX_VALUE,
    min: Number.MIN_VALUE,
    swipeSensitivity: 0.9,
});

const emits = defineEmits<{
    changed: [value: number];
}>();

const internalScore = ref(ability.score);
const swipeInput = ref<HTMLDivElement>();


const height = computed(() => swipeInput.value?.offsetHeight || 20);
let startScore = -1;

const { lengthY } = useSwipe(swipeInput, {
    onSwipeStart() {
        startScore = internalScore.value;
    },
    
    onSwipe() {
        const amount = (lengthY.value / height.value) * swipeSensitivity;
        internalScore.value = Math.round(Math.min(Math.max(startScore + amount, min), max));
    },
    
    onSwipeEnd() {
        const amount = (lengthY.value / height.value) * swipeSensitivity;
        const result = Math.round(Math.min(Math.max(startScore + amount, min), max));
        
        emits('changed', result);
        internalScore.value = result;
    },
});

function change(amount: number) {
    const result = Math.min(Math.max(internalScore.value + amount, min), max);
    internalScore.value = result;
    emits('changed', result);
}
</script>

<template>
    <div ref="swipeInput" 
         class="flex flex-row input input-bordered text-3xl font-extrabold select-none justify-center max-w-full">
        <input type="number"
               :value="internalScore"
               class="max-w-12 text-center" />
        <div class="flex flex-col">
            <div class="btn btn-ghost btn-xs btn-square max-w-4"
                 @click="change(1)">
                <span class="mdi mdi-chevron-up"></span>
            </div>
            <div class="btn btn-ghost btn-xs btn-square max-w-4"
                 @click="change(-1)">
                <span class="mdi mdi-chevron-down"></span>
            </div>
        </div>
    </div>
</template>

<style scoped>

</style>