<template>
    <div class="rounded bg-secondary text-secondary-content divide-y">
        <div class="flex justify-between px-2 pt-1">
            <div>{{ message.sender.login }}</div>
            <div>{{ message.rollType }}</div>
            <div class="text-xs font-light opacity-50">{{ formatDate(message.created) }}</div>
        </div>
        <div>
            <ul class="grid grid-cols-10 px-2">
                <template v-for="result in message.result.results" :key="result.value">
                    <li>
                        <div class="image-container">
                            <img src="@/assets/d20.png" :alt="result.type" />
                            <div class="number-overlay text-2xl text-amber-200 font-bold">
                                {{ result.value }}
                            </div>
                        </div>
                    </li>
                </template>
                <li v-if="message.result.modifier" class="text-2xl text-amber-200 font-bold">
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
.number-overlay {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
}

.image-container {
    position: relative;
    text-align: center;
    width: 32px;
    height: 32px;
}
</style>
