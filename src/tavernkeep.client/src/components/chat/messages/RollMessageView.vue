<template>
    <div class="rounded bg-[#460c084b] divide-y">
        <div class="flex justify-between px-2 pt-1">
            <div>{{ message.sender.login }}</div>
            <div>{{ message.rollType }}</div>
            <div class="text-xs font-light opacity-50">{{ formatDate(message.created) }}</div>
        </div>
        <div>
            <ul class="grid grid-cols-10 px-2 my-2">
                <template v-for="result in message.result.results" :key="result.value">
                    <li>
                        <DiceIcon :die="result.type" :value="result.value" class="w-8" />
                    </li>
                </template>
                <li v-if="message.result.modifier" class="text-2xl font-thin">
                    {{ message.result.modifier > 0 ? '+' + message.result.modifier : message.result.modifier }}
                </li>
            </ul>
        </div>
        <div class="flex text-3xl justify-center p-4">
            <span :data-tip="message.expression" class="tooltip">{{ message.result.value }}</span>
        </div>
    </div>
</template>
<script setup lang="ts">
import DiceIcon from '@/components/DiceIcon.vue';
import { RollMessage } from '@/entities/Message';
const { message } = defineProps<{
    message: RollMessage;
}>();

function formatDate(dateString: Date): string {
    const date = new Date(dateString.toString());
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const seconds = date.getSeconds().toString().padStart(2, '0');

    return `${hours}:${minutes}:${seconds}`;
}
</script>
<style scoped></style>
