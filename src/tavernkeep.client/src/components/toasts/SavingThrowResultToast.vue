<script setup lang="ts">
import type { SavingThrowRollMessage } from '@/entities';

const { message } = defineProps<{ message: SavingThrowRollMessage }>();
</script>

<template>
    <div class="flex flex-row items-center justify-center gap-4">
        <div class="flex flex-col">
            <span class="text-xl font-bold">SAVE</span>
            <span>{{ message.savingThrow.name }}</span>
        </div>
        <div class="flex flex-col grow">
            <span class="self-end text-4xl font-bold">{{ message.result.value }}</span>
        </div>
    </div>
</template>

<style scoped></style>
