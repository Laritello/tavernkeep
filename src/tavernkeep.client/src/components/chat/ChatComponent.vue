<template>
    <div class="flex flex-col">
        <h1 class="text-xl p-2">Chat</h1>
        <div class="flex grow overflow-auto" v-chat-scroll="{ always: false, smooth: true }">
            <div class="w-full px-4">
                <ChatMessageView v-for="item in appStore.messages.list" :key="item.id" :message="item" />
            </div>
        </div>
        <div class="">
            <UserSelector v-model="selectedUserId" :users="appStore.users.listExceptCurrent" />
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
import { RollType } from '@/contracts/enums/RollType';
import { useAppStore } from '@/stores/app.store';

// Components
import UserSelector from './UserSelector.vue';
import ChatMessageView from './messages/ChatMessageView.vue';
import CommandInput from './CommandInput.vue';

const appStore = useAppStore();

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
        await appStore.messages.createRollMessage(expression, rollType);
    } else {
        const privateMessageRecipient = selectedUserId.value || undefined;
        await appStore.messages.createMessage(message.value, privateMessageRecipient);
    }

    message.value = '';
}
</script>

<style scoped></style>
