<template>
    <div class="flex flex-col h-full">
        <h1 class="text-xl p-2">Chat</h1>
        <div class="flex grow overflow-auto">
            <div class="container px-4">
                <template v-for="item in messagesStore.messages" :key="item">
                    <MessageComponent :message="item" />
                </template>
            </div>
        </div>
        <div class="">
            <UserSelector v-model="selectedUser" :users="usersStore.users.filter((u) => u.login != auth.userName)" />
            <form @submit.prevent="sendMessage" class="m-2">
                <div class="join w-full">
                    <input
                        type="text"
                        v-model="message"
                        placeholder="Type here..."
                        class="input input-bordered input-md w-full max-w-xl"
                    />
                    <button type="submit" icon="mdi-send" variant="text" rounded="0" size="large" class="btn btn-ghost">
                        <v-icon icon="mdi-send" />
                    </button>
                </div>
            </form>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import ChatHub from '@/api/hubs/ChatHub';
import { useMessagesStore } from '@/stores/messages.store';
import type { Message } from '@/entities/Message';
import UserSelector from './UserSelector.vue';
import { useUsersStore } from '@/stores/users.store';
import type { User } from '@/entities/User';
import { useAuthStore } from '@/stores/auth.store';
import MessageComponent from './MessageComponent.vue';

const auth = useAuthStore();
const messagesStore = useMessagesStore();
const usersStore = useUsersStore();

const message = ref('');
const selectedUser = ref<User>();

onMounted(async () => {
    await messagesStore.fetchMessages(0, 20);
    ChatHub.connection.on('ReceiveMessage', (msg: Message) => {
        console.log('Message Received: ' + msg.content);
        messagesStore.messages.unshift(msg);
    });
});

async function sendMessage() {
    const privateMessageRecipient = selectedUser.value?.id || undefined;
    await messagesStore.createMessage(message.value, privateMessageRecipient);
    message.value = '';
}
</script>

<style scoped>
.box {
    @apply flex flex-col;
}

.box .row.auto {
    flex: 0 1 auto;
}

.box .row.fill {
    flex-flow: column;
    flex: 1 1 auto;
    overflow-y: auto;
}

.box .row.fixed {
    flex: 0 1 40px;
}
</style>
