<template>
    <div class="pb-2">
        <div class="w-full">
            <TextMessageView v-if="message instanceof TextMessage" :message="message" :color="messageColor(message)" />
            <RollMessageView
                v-else-if="message instanceof RollMessage"
                :message="message"
                :color="rollColor(message)"
            />
            <span v-else>Unknown message type: {{ message.$type }}</span>
        </div>
    </div>
</template>

<script setup lang="ts">
import { type Message } from '@/entities/Message';
import { TextMessage } from '@/entities/Message';
import { RollMessage } from '@/entities/Message';
import { RollType } from '@/contracts/enums/RollType';

import TextMessageView from './TextMessageView.vue';
import RollMessageView from './RollMessageView.vue';

const { message } = defineProps<{
    message: Message;
}>();

function messageColor(message: TextMessage) {
    return message.isPrivate ? 'deep-purple' : 'primary';
}

function rollColor(message: RollMessage) {
    return message.rollType == RollType.Public ? 'green' : 'coral';
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
