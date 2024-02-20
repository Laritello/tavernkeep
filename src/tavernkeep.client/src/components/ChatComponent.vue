<template>
    <div class="box">
        <div class="row auto">
            <v-sheet class="pa-1">
                <div class="text-h6">Chat</div>
            </v-sheet>
        </div>
        <div class="row fill">
            <v-container>
                <template v-for="item in messagesStore.messages" :key="item">
                    <MessageComponent :message="item" />
                </template>
            </v-container>
        </div>
        <div>
            <UserSelector v-model="selectedUser" :users="usersStore.users.filter((u) => u.login != auth.userName)" />
            <div class="row fixed">
                <v-sheet class="py-4">
                    <v-form @submit.prevent="sendMessage">
                        <v-row class="px-4">
                            <v-text-field v-model="message" variant="outlined" />
                            <v-btn type="submit" icon="mdi-send" variant="text" rounded="0" size="large"></v-btn>
                        </v-row>
                    </v-form>
                </v-sheet>
            </div>
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
    display: flex;
    flex-flow: column;
    height: 100%;
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
