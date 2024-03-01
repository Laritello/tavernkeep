<template>
    <div class="flex flex-col h-full">
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
                    <input
                        type="text"
                        v-model="message"
                        placeholder="Type here..."
                        class="input input-bordered input-md w-full max-w-xl"
                    />
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

const messagesStore = useMessagesStore();
const usersStore = useUsersStore();

const message = ref('');
const selectedUserId = ref<string>();

async function sendMessage() {
    if (message.value.startsWith('/roll')) {
        const rollExpression = message.value.replace('/roll', '').trim();
        await messagesStore.createRollMessage(rollExpression);
    } else {
        const privateMessageRecipient = selectedUserId.value || undefined;
        await messagesStore.createMessage(message.value, privateMessageRecipient);
    }

    message.value = '';
}
</script>

<style scoped></style>
