<template>
    <div class="pb-2 w-full">
        <component :is="getComponentByType(message)" :message="message" :align-right="isUserSender">
            Unknown message type: {{ message.$type }}
        </component>
    </div>
</template>

<script setup lang="ts">
import { computed } from 'vue';

import { type Message } from '@/entities/Message';

import TextMessageView from './TextMessageView.vue';
import RollMessageView from './RollMessageView.vue';
import SkillRollMessageView from './SkillRollMessageView.vue';

import { useAppStore } from '@/stores/app.store';

const { message } = defineProps<{
    message: Message;
}>();

const appStore = useAppStore();
const isUserSender = computed(() => appStore.users.current?.id === message.sender.id);

function getComponentByType(message: Message) {
    switch (message.$type) {
        case 'TextMessage':
            return TextMessageView;
        case 'RollMessage':
            return RollMessageView;
        case 'SkillRollMessage':
            return SkillRollMessageView;
        default:
            return `div`;
    }
}
</script>

<style scoped></style>
