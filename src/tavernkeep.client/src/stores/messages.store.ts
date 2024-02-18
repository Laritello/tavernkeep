import { ref } from 'vue';
import { defineStore } from 'pinia';

import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { ApiClient } from '@/api/base/ApiClient';
import type { Message } from '@/entities/Message';
import type { MessageType } from '@/contracts/enums/MessageType';

const api: ApiClient = ApiClientFactory.createApiClient();
export const useMessagesStore = defineStore('messages.store', () => {
    const messages = ref<Message[]>([]);

    async function fetchMessages(skip: number, take: number) {
        const messagesResponse = await api.getMessages(skip, take);
        messages.value.push(...messagesResponse.data);
    }

    async function createMessage(message: string, recipientId?: string) {
        const response = await api.sendMessage(message, recipientId);
        if (!response.isSuccess()) {
            console.error(response.statusText);
        }
    }

    return { messages, fetchMessages, createMessage };
});
