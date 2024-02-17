import { ref } from 'vue';
import { defineStore } from 'pinia';

import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { ApiClient } from '@/api/base/ApiClient';
import type { Message } from '@/entities/Message';
import type { MessageType } from '@/contracts/enums/MessageType';

const api: ApiClient = ApiClientFactory.createApiClient();
export const useRoomMessagesStore = defineStore('roomMessages', () => {
    const messages = ref<Message[]>([]);

    async function fetchMessages(skip: number, take: number) {
        const messagesResponse = await api.getMessages(skip, take);
        messages.value.push(...messagesResponse.data);
    }

    async function createMessage(message: string, type: MessageType) {
        const response = await api.sendMessage(message, type);
        if (!response.isSuccess()) {
            console.error(response.statusText);
        }
    }

    return { messages, fetchMessages, createMessage };
});
