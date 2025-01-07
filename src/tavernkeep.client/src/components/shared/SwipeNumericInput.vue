<script setup lang="ts">
import { ref, computed } from 'vue';
import { useSwipe } from '@vueuse/core';
import type { Ability } from '@/contracts/character';

const { ability } = defineProps<{
    ability: Ability;
}>();

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
        const amount = (lengthY.value / height.value) * 0.9;
        if(startScore + amount >= 21 || startScore + amount < 8) {
            return;
        }

        emits('changed', Math.floor(startScore + amount));
        internalScore.value = Math.floor(startScore + amount);
    },
});

function increase(amount: number) {
    if (internalScore.value >= 20) return;
    emits('changed', internalScore.value + amount);
    internalScore.value += amount;
}

function decrease(amount: number) {
    if (internalScore.value <= 8) return;
    emits('changed', internalScore.value - amount);
    internalScore.value -= amount;
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
                 @click="increase(1)">
                <span class="mdi mdi-chevron-up"></span>
            </div>
            <div class="btn btn-ghost btn-xs btn-square max-w-4"
                 @click="decrease(1)">
                <span class="mdi mdi-chevron-down"></span>
            </div>
        </div>
    </div>
</template>

<style scoped>

</style>