<template>
    <div class="message-container pb-4">
        <div>
            <v-avatar size="small" color="primary">
                {{ message.sender.login.slice(0, 2) }}
            </v-avatar>
        </div>
        <div>
            <v-sheet :color="messageColor()" rounded>
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
            </v-sheet>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { Message } from '@/entities/Message';

const { message } = defineProps<{
    message: Message;
}>();

function messageColor() {
    return message.isPrivate ? 'deep-purple' : 'primary';
}

function formatDate(dateString: Date): string {
    const date = new Date(dateString.toString());
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const seconds = date.getSeconds().toString().padStart(2, '0');

    return `${hours}:${minutes}:${seconds}`;
}
</script>

<style scoped>
.message-bubble {
    display: grid;
    grid-template-rows: min-content min-content;
    grid-gap: 0px;
}

.message-container {
    display: grid;
    grid-template-columns: min-content 1fr;
    grid-gap: 5px;
}

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
