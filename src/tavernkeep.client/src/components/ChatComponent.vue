<template>
    <div class="box">
        <div class="row auto">
            <v-sheet class="pa-1">
                <div class="text-h6">Chat</div>
            </v-sheet>
        </div>
        <div class="row fill">
            <v-container>
                <v-infinite-scroll id="message-list" :items="messages" side="end" mode="manual" :onLoad="infiniteLoad">
                    <template v-for="item in messages" :key="item">
                        <v-card class="mx-1 my-3">
                            <v-card-title>
                                <div>
                                    <v-row align="center">
                                        <v-col cols="3">
                                            <v-avatar color="primary"> {{ item.sender.login.slice(0, 2).toUpperCase()
                                            }}</v-avatar>
                                        </v-col>
                                        <v-col cols="5">
                                            <div class="text-caption">{{ item.sender.login }}</div>
                                        </v-col>
                                        <v-col cols="4">
                                            <div class="text-caption">{{ item.created }}</div>
                                        </v-col>
                                    </v-row>
                                </div>
                            </v-card-title>
                            <v-card-text class="text-body-1"> {{ item.content }}</v-card-text>
                        </v-card>
                    </template>
                </v-infinite-scroll>
            </v-container>
        </div>
        <div class="row fixed">
            <v-sheet class="pa-1">
                <v-textarea density="compact" variant="outlined" append-inner-icon="mdi-send" v-model="message" rows="3"
                    row-height="15" no-resize @click:append-inner="sendMessage"></v-textarea>
            </v-sheet>
        </div>
    </div>
</template>

<script lang="ts">
import type { ApiClient } from '@/api/base/ApiClient';
import { MessageType } from '@/contracts/enums/MessageType';
import type { Message } from '@/entities/Message';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import ChatHub from '@/api/hubs/ChatHub';
import type { ApiResponse } from '@/api/base/ApiResponse';

const client: ApiClient = ApiClientFactory.createApiClient();

interface ChatModel {
    message: string,
    messages: Message[]
}

interface LoadEvent {
    side: 'start' | 'end' | 'both'
    done: (status: 'loading' | 'error' | 'empty' | 'ok') => void
}

export default {
    setup() {
        return { MessageType }
    },

    data(): ChatModel {
        return {
            message: '',
            messages: []
        }
    },

    async mounted() {
        const response = await client.getMessages(0, 20);
        this.messages.push(...response.data)
        
        ChatHub.connection.on("ReceiveMessage", (msg: Message) => {
            console.log("Message Received: " + msg.content)
            this.messages.unshift(msg)
        })
    },

    methods: {
        async sendMessage() {
            await client.sendMessage(this.message, MessageType.Text);
        },
        async loadMessages(): Promise<ApiResponse<Message[]>> {
            return await client.getMessages(this.messages.length, 20);
        },
        async infiniteLoad(event: LoadEvent) {
            event.side = 'start'
            const response = await this.loadMessages()
            event.done(response.data.length >= 20 ? 'ok' : 'empty');
            this.messages.push(...response.data)
        }
    }
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