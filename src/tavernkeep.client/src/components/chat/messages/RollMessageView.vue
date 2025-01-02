<template>
    <div class="rounded-3xl bg-neutral relative">
        <div class="flex justify-between px-2 pt-1 text-neutral-content">
            <div>{{ message.sender.login }}</div>
            <div>{{ message.rollType }}</div>
        </div>
        <div>
            <ul class="grid grid-cols-10 px-2 my-2 text-neutral-content">
                <template v-for="result in message.result.results" :key="result.value">
                    <li>
                        <DiceIcon :die="result.type" :value="result.value" class="w-8" />
                    </li>
                </template>
                <li v-if="message.result.modifier" class="text-2xl font-thin text-neutral-content">
                    {{ message.result.modifier > 0 ? '+' + message.result.modifier : message.result.modifier }}
                </li>
            </ul>
        </div>
        <div class="flex text-3xl justify-center p-4 text-neutral-content">
            <span :data-tip="message.expression" class="tooltip">{{ message.result.value }}</span>
        </div>
        <div class="avatar placeholder absolute bottom-0 right-0">
            <div class="bg-neutral text-neutral-content w-10 rounded-full border-2 border-white">
                <span>{{ message.sender.login.slice(0, 2) }}</span>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import DiceIcon from '@/components/DiceIcon.vue';
import type { RollMessage } from '@/entities/Message';
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
