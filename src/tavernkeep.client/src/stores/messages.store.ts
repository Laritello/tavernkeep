import { ref } from 'vue';
import { defineStore } from 'pinia';
import { plainToInstance } from 'class-transformer';

import ChatHub from '@/api/hubs/ChatHub';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { ApiClient } from '@/api/base/ApiClient';
import { Message, RollMessage, TextMessage } from '@/entities/Message';
import type { RollType } from '@/contracts/enums/RollType';

export const useMessagesStore = defineStore('messages.store', () => {
    const api: ApiClient = ApiClientFactory.createApiClient();
    ChatHub.connection.on('ReceiveMessage', (msg: Message) => {
        console.log(msg);
        if (!msg.$type) {
            console.warn('Message $type is undefined');
        }
        appendMessage(msg);
    });

    const messages = ref<Message[]>([]);

    async function fetchMessages(skip: number, take: number) {
        const messagesResponse = await api.getMessages(skip, take);
        messages.value = messagesResponse.data;
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

    function appendMessage(message: Message) {
        let typedMessage = message;

        switch (message.$type) {
            case 'TextMessage':
                typedMessage = plainToInstance(TextMessage, message);
                break;
            case 'RollMessage':
                typedMessage = plainToInstance(RollMessage, message);
                break;
            default:
                typedMessage = plainToInstance(Message, message);
        }
        messages.value.push(typedMessage);
    }

    return { messages, fetchMessages, createMessage, createRollMessage };
});
