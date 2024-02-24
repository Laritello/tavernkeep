<template>
    <div class="flex flex-col h-full">
        <h1 class="text-xl p-2">Chat</h1>
        <div ref="chatView" class="flex grow overflow-auto">
            <div class="container px-4">
                <template v-for="item in messagesStore.messages" :key="item">
                    <ChatBubble :message="item" />
                </template>
            </div>
        </div>
        <div class="">
            <UserSelector v-model="selectedUserId" :users="usersStore.users.filter((u) => u.login != auth.userName)" />
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
import { ref, onMounted, nextTick } from 'vue';
import ChatHub from '@/api/hubs/ChatHub';
import type { Message } from '@/entities/Message';

// Components
import UserSelector from './UserSelector.vue';
import ChatBubble from './messages/ChatBubble.vue';

// Stores
import { useMessagesStore } from '@/stores/messages.store';
import { useUsersStore } from '@/stores/users.store';
import { useAuthStore } from '@/stores/auth.store';

const auth = useAuthStore();
const messagesStore = useMessagesStore();
const usersStore = useUsersStore();

const message = ref('');
const selectedUserId = ref<string>();
const chatView = ref<HTMLElement>();

messagesStore.fetchMessages(0, 20);
ChatHub.connection.on('ReceiveMessage', (msg: Message) => {
    messagesStore.appendMessage(msg);
});

onMounted(async () => {
    await scrollToBottom();
});

async function sendMessage() {
    const privateMessageRecipient = selectedUserId.value || undefined;
    await messagesStore.createMessage(message.value, privateMessageRecipient);
    message.value = '';
    await scrollToBottom();
}

async function scrollToBottom() {
    await nextTick(() => {
        if (chatView.value) {
            chatView.value.scrollTop = chatView.value.scrollHeight;
        }
    });
}
</script>

<style scoped></style>
