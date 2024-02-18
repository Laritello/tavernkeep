<template>
    <div class="box">
        <div class="row auto">
            <v-sheet class="pa-1">
                <div class="text-h6">Chat</div>
            </v-sheet>
        </div>
        <div class="row fill">
            <v-container>
                <template
                    v-for="item in messagesStore.messages"
                    :key="item"
                >
                    <v-card class="mx-1 my-3">
                        <v-card-title>
                            <div>
                                <v-row v-if="item.isPrivate">
                                    <div class="text-caption">Private from: </div>
                                    <div class="text-caption font-weight-bold ml-1"> {{ item.sender.login }}</div>
                                </v-row>
                                <v-row align="center">
                                    <v-col cols="3">
                                        <v-avatar color="primary">
                                            {{
                                                item.sender.login
                                                    .slice(0, 2)
                                                    .toUpperCase()
                                            }}</v-avatar
                                        >
                                    </v-col>
                                    <v-col cols="5">
                                        <div class="text-caption">
                                            {{ item.sender.login }}
                                        </div>
                                    </v-col>
                                    <v-col cols="4">
                                        <div class="text-caption">
                                            {{ item.created }}
                                        </div>
                                    </v-col>
                                </v-row>
                            </div>
                        </v-card-title>
                        <v-card-text class="text-body-1">
                            {{ item.content }}</v-card-text
                        >
                    </v-card>
                </template>
            </v-container>
        </div>
        <div class="row fixed">
            <v-sheet class="py-4">
                <v-form @submit.prevent="sendMessage">
                    <v-row class="px-4">
                        <v-text-field v-model="message" variant="outlined" />
                        <v-btn
                            type="submit"
                            icon="mdi-send"
                            variant="text"
                            rounded="0"
                            size="large"
                        ></v-btn>
                    </v-row>
                </v-form>
            </v-sheet>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import ChatHub from '@/api/hubs/ChatHub';
import { useMessagesStore } from '@/stores/messages.store';
import type { Message } from '@/entities/Message';

const messagesStore = useMessagesStore();

const message = ref('');

onMounted(async () => {
    await messagesStore.fetchMessages(0, 20);
    ChatHub.connection.on('ReceiveMessage', (msg: Message) => {
        console.log('Message Received: ' + msg.content);
        messagesStore.messages.unshift(msg);
    });
});

async function sendMessage() {
    await messagesStore.createMessage(message.value);
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
@/stores/room.messages.store@/stores/messages.store