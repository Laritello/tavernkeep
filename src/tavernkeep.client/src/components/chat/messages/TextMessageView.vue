<template>
    <div class="chat" :class="{ 'chat-end': alignRight, 'chat-start': !alignRight }">
        <div class="chat-image avatar placeholder">
            <div class="bg-neutral text-neutral-content w-10 rounded-full">
                <img v-if="message.characterId" alt="Character portrait" :src="`${api.baseURL}portraits/${message.characterId}`" />
                <span v-else>{{ message.displayName.slice(0, 2) }}</span>
            </div>
        </div>
        <div class="chat-header">
            {{ message.displayName }}
            <time class="text-xs opacity-50">{{ formatDate(message.created) }}</time>
        </div>
        <div class="chat-bubble">{{ message.text }}</div>
        <div v-if="message.isPrivate" class="chat-footer opacity-50">
            <div class="private">
                <span class="body-1 font-weight-light mr-1">Private for {{ message.recipient?.login }}</span>
                <span>👁‍</span>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import type { AxiosApiClient } from '@/api/axios/AxiosApiClient';
import type { TextMessage } from '@/entities/Message';
import { ApiClientFactory } from '@/factories/ApiClientFactory';

// TODO: Find a better way to pass api base URL
const api: AxiosApiClient = ApiClientFactory.createApiClient();

const { message } = defineProps<{
    message: TextMessage;
    alignRight: boolean;
}>();

function formatDate(dateString: Date): string {
    const date = new Date(dateString.toString());
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    //const seconds = date.getSeconds().toString().padStart(2, '0');

    //return `${hours}:${minutes}:${seconds}`;
    return `${hours}:${minutes}`;
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

.private {
    display: flex;
    justify-content: flex-end;
    align-items: center;
}
</style>
