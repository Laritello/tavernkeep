<template>
    <div class="pb-2 w-full">
        <!-- prettier-ignore -->
        <TextMessageView v-if="(message instanceof TextMessage)" :message="message" :align-right="isUserSender" />
        <!-- prettier-ignore -->
        <RollMessageView v-else-if="(message instanceof RollMessage)" :message="message" />
        <!-- prettier-ignore -->
        <SkillRollMessageView v-else-if="message instanceof SkillRollMessage" :message="message" />
        <span v-else>Unknown message type: {{ message.$type }}</span>
    </div>
</template>

<script setup lang="ts">
import { computed } from 'vue';

import { Message } from '@/entities/Message';
import { TextMessage } from '@/entities/Message';
import { RollMessage } from '@/entities/Message';
import { SkillRollMessage } from '@/entities/Message';

import TextMessageView from './TextMessageView.vue';
import RollMessageView from './RollMessageView.vue';
import SkillRollMessageView from './SkillRollMessageView.vue';

import { useAppStore } from '@/stores/app.store';

const { message } = defineProps<{
    message: Message;
}>();

const appStore = useAppStore();
const isUserSender = computed(() => appStore.users.current?.id === message.sender.id);
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
