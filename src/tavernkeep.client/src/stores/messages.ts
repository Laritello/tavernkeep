import { defineStore } from 'pinia';
import { computed, ref } from 'vue';

import type { AxiosApiClient } from '@/api/axios/AxiosApiClient';
import ChatHub from '@/api/hubs/ChatHub';
import type { RollType } from '@/contracts/enums/RollType';
import type { Message } from '@/entities/Message';
import { ApiClientFactory } from '@/factories/ApiClientFactory';

export const useMessages = defineStore('messages.store', () => {
    const api: AxiosApiClient = ApiClientFactory.createApiClient();
    ChatHub.connection.on('ReceiveMessage', (msg: Message) => {
        appendMessage(msg);
    });

    ChatHub.connection.on('DeleteMessage', async (msg: Message) => {
        const index = messageList.value.findIndex((m) => m.id === msg.id);
        if (index !== -1) {
            messageList.value.splice(index, 1);
        }
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
