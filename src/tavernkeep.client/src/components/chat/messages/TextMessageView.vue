<template>
    <div
        class="chat"
        :class="{
            'chat-end': alignRight,
            'chat-start': !alignRight,
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

const { message } = defineProps<{
    message: TextMessage;
    alignRight: boolean;
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
