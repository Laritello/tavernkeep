import { ref } from 'vue';
import { defineStore } from 'pinia';

import ChatHub from '@/api/hubs/ChatHub';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { ApiClient } from '@/api/base/ApiClient';
import type { Message } from '@/entities/Message';
import type { RollType } from '@/contracts/enums/RollType';

export const useMessagesStore = defineStore('messages.store', () => {
    const api: ApiClient = ApiClientFactory.createApiClient();
    ChatHub.connection.on('ReceiveMessage', (msg: Message) => {
        if (!msg.$type) {
            console.log(msg);
            console.warn('Message $type is undefined');
        }
        appendMessage(msg);
    });

    const all = ref<Message[]>([]);

    async function fetch(skip: number, take: number) {
        const messagesResponse = await api.getMessages(skip, take);
        all.value = messagesResponse.data;
    }

    async function createMessage(message: string, recipientId?: string) {
        const response = await api.sendMessage(message, recipientId);
        if (!response.isSuccess()) {
            console.error(response.statusText);
        }
    }

    async function createRollMessage(expression: string, rollType: RollType) {
        const response = await api.sendRollMessage(expression, rollType);
        if (!response.isSuccess()) {
            console.error(response.statusText);
        }
    }

    async function deleteMessage(messageId: string) {
        const response = await api.deleteMessage(messageId);
        if (!response.isSuccess()) {
            console.error(response.statusText);
        }
    }

    function appendMessage(message: Message) {
        all.value.push(message);
    }

    return { all, fetch, createMessage, deleteMessage, createRollMessage };
});
