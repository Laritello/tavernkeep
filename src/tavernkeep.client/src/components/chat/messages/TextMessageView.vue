<template>
    <!-- <v-sheet :color="color || 'primary'" rounded>
        <div class="text-container px-2 pb-1">
            <div class="header px-1 pt-1">
                <div class="body-1 font-weight-medium">{{ message.sender.login }}</div>
                <div class="body-1 font-weight-light">{{ formatDate(message.created) }}</div>
            </div>
            <div class="pb-1" style="align-self: flex-end">
                <div v-if="message.isPrivate" class="private">
                    <div class="body-1 font-weight-light mr-1">Private</div>
                    <v-icon size="x-small" icon="mdi-eye"></v-icon>
                </div>
            </div>
            <div>
                <div class="body-1 pl-1">{{ message.text }}</div>
            </div>
        </div>
    </v-sheet> -->

    <div
        class="chat"
        :class="{
            'chat-end': message.sender.login === auth.userName,
            'chat-start': message.sender.login !== auth.userName,
        }"
    >
        <div class="chat-image">
            <v-avatar size="small" color="primary">
                {{ message.sender.login.slice(0, 2) }}
            </v-avatar>
        </div>
        <div class="chat-header">
            {{ message.sender.login }}
            <time class="text-xs opacity-50">{{ formatDate(message.created) }}</time>
        </div>
        <div class="chat-bubble">{{ message.text }}</div>
        <div class="chat-footer opacity-50">
            <div v-if="message.isPrivate" class="private">
                <span class="body-1 font-weight-light mr-1">Private for {{ message.recipient?.login }}</span>
                <v-icon size="x-small" icon="mdi-eye"></v-icon>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { TextMessage } from '@/entities/Message';
import { useAuthStore } from '@/stores/auth.store';

const auth = useAuthStore();

const { message } = defineProps<{
    message: TextMessage;
    color?: string;
}>();

function formatDate(dateString: Date): string {
    const date = new Date(dateString.toString());
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const seconds = date.getSeconds().toString().padStart(2, '0');

    return `${hours}:${minutes}:${seconds}`;
}
</script>

<style scoped>
.text-container {
    display: grid;
    grid-template-rows: min-content min-content 1fr;
    grid-gap: 0px;
}

.text-container .header {
    display: grid;
    grid-template-columns: 1fr min-content;
    grid-gap: 3px;
    align-items: center;
}

.private {
    display: flex;
    justify-content: flex-end;
    align-items: center;
}
</style>
