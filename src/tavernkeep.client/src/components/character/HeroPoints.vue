<script setup lang="ts">
const { amount } = defineProps<{
    amount: number;
}>();

const emits = defineEmits<{
    changed: [value: number];
}>();

function setHeroPoints(index: number) {
    if (index == amount) {
        emits('changed', index - 1);
    } else {
        emits('changed', index);
    }
}
</script>

<template>
    <div class="flex flex-row gap-1 justify-center">
        <svg
            class="w-6 h-6"
            fill="#efbf04"
            viewBox="0 -960 960 960"
            xmlns="http://www.w3.org/2000/svg"
            @click="setHeroPoints(1)"
        >
            <path
                transform="matrix(.85391 .044751 -.044751 .85391 344.43 243.03)"
                d="m378.35-444.99-485.23 25.016-264.28-407.72 220.95-432.73 485.23-25.016 264.28 407.72z"
                :fill-opacity="amount >= 1 ? 1.0 : 0"
                stroke="currentColor"
                stroke-width="93.558"
            />
        </svg>

        <svg
            class="w-6 h-6"
            fill="#efbf04"
            viewBox="0 -960 960 960"
            xmlns="http://www.w3.org/2000/svg"
            @click="setHeroPoints(2)"
        >
            <path
                transform="matrix(.85391 .044751 -.044751 .85391 344.43 243.03)"
                d="m378.35-444.99-485.23 25.016-264.28-407.72 220.95-432.73 485.23-25.016 264.28 407.72z"
                :fill-opacity="amount >= 2 ? 1.0 : 0"
                stroke="currentColor"
                stroke-width="93.558"
            />
        </svg>

        <svg
            class="w-6 h-6"
            fill="#efbf04"
            viewBox="0 -960 960 960"
            xmlns="http://www.w3.org/2000/svg"
            @click="setHeroPoints(3)"
        >
            <path
                transform="matrix(.85391 .044751 -.044751 .85391 344.43 243.03)"
                d="m378.35-444.99-485.23 25.016-264.28-407.72 220.95-432.73 485.23-25.016 264.28 407.72z"
                :fill-opacity="amount >= 3 ? 1.0 : 0"
                stroke="currentColor"
                stroke-width="93.558"
            />
        </svg>
    </div>
</template>

<style scoped></style>
