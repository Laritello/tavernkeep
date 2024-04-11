import { computed, ref } from 'vue';
import { defineStore } from 'pinia';

import ChatHub from '@/api/hubs/ChatHub';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { AxiosApiClient } from '@/api/axios/AxiosApiClient';
import type { Message } from '@/entities/Message';
import type { RollType } from '@/contracts/enums/RollType';

export const useMessagesStore = defineStore('messages.store', () => {
    const api: AxiosApiClient = ApiClientFactory.createApiClient();
    ChatHub.connection.on('ReceiveMessage', (msg: Message) => {
        appendMessage(msg);
    });

    const messageList = ref<Message[]>([]);
    const list = computed(() => messageList.value);

    async function fetch(skip: number, take: number) {
        messageList.value = await api.getMessages(skip, take);
    }

    async function createMessage(message: string, recipientId?: string) {
        await api.sendMessage(message, recipientId);
    }

    async function createRollMessage(expression: string, rollType: RollType) {
        await api.sendRollMessage(expression, rollType);
    }

    async function deleteMessage(messageId: string) {
        await api.deleteMessage(messageId);
    }

    function appendMessage(message: Message) {
        messageList.value.push(message);
    }

    return { list, fetch, createMessage, deleteMessage, createRollMessage };
});
