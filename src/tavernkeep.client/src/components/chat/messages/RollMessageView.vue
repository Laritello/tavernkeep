<template>
    <div class="rounded bg-secondary text-secondary-content divide-y">
        <div class="flex justify-between px-2 pt-1">
            <div>{{ message.sender.login }}</div>
            <div>{{ message.rollType }}</div>
            <div class="text-xs font-light opacity-50">{{ formatDate(message.created) }}</div>
        </div>
        <div>
            <ul class="grid grid-cols-5 px-2">
                <template v-for="result in message.result.results" :key="result.value">
                    <li>{{ result.type }} = {{ result.value }}</li>
                </template>
                <li>{{ message.result.modifier > 0 ? '+' + message.result.modifier : message.result.modifier }}</li>
            </ul>
        </div>
        <div class="flex text-3xl justify-center p-4">
            <span :data-tip="message.expression" class="tooltip">{{ message.result.value }}</span>
        </div>
    </div>
</template>
<script setup lang="ts">
import { RollMessage } from '@/entities/Message';
const { message } = defineProps<{
    message: RollMessage;
    color?: string;
}>();

function formatDate(dateString: Date): string {
    const date = new Date(dateString.toString());
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const seconds = date.getSeconds().toString().padStart(2, '0');

    return `${hours}:${minutes}:${seconds}`;
}
</script>
<style scoped>
.text-container {
    display: grid;
    grid-template-rows: min-content min-content 1fr;
    grid-gap: 0px;
}

.text-container .header {
    display: grid;
    grid-template-columns: 1fr min-content;
    grid-gap: 3px;
    align-items: center;
}
</style>
