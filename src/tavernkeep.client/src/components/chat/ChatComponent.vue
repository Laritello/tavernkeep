<template>
    <div class="flex flex-col">
        <h1 class="text-xl p-2">Chat</h1>
        <div class="flex grow overflow-auto" v-chat-scroll="{ always: false, smooth: true }">
            <div class="w-full px-4">
                <template v-for="item in messagesStore.messages" :key="item">
                    <ChatBubble :message="item" />
                </template>
            </div>
        </div>
        <div class="">
            <UserSelector v-model="selectedUserId" :users="usersStore.otherUsers" />
            <form @submit.prevent="sendMessage" class="m-2">
                <div class="join w-full">
                    <CommandInput v-model="message" :commands="slashCommands" class="w-full" />
                    <button type="submit" class="btn btn-ghost">
                        <v-icon icon="mdi-send" />
                    </button>
                </div>
            </form>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

// Components
import UserSelector from './UserSelector.vue';
import ChatBubble from './messages/ChatBubble.vue';

// Stores
import { useMessagesStore } from '@/stores/messages.store';
import { useUsersStore } from '@/stores/users.store';
import CommandInput from './CommandInput.vue';
import { RollType } from '@/contracts/enums/RollType';

const messagesStore = useMessagesStore();
const usersStore = useUsersStore();

const message = ref('');
const selectedUserId = ref<string>();

const slashCommands = [
    {
        name: '/roll',
        description: 'Make public roll in chat',
    },
    {
        name: '/sroll',
        description: 'Same as /roll but result will be seen only by GM',
    },
    {
        name: '/proll',
        description: 'Same as /roll but result will be seen only by GM and you',
    },
];

async function sendMessage() {
    if (message.value.startsWith('/')) {
        const [command, expression] = message.value.split(' ', 2);
        let rollType = RollType.Public;
        switch (command) {
            case '/roll':
                break;
            case '/sroll':
                rollType = RollType.Secret;
                break;
            case '/proll':
                rollType = RollType.Private;
                break;
            default:
                console.warn('Unknown command');
        }
        await messagesStore.createRollMessage(expression, rollType);
    } else {
        const privateMessageRecipient = selectedUserId.value || undefined;
        await messagesStore.createMessage(message.value, privateMessageRecipient);
    }

    message.value = '';
}
</script>

<style scoped></style>
