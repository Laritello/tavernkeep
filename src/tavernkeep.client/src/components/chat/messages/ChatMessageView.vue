<script setup lang="ts">
import { computed } from 'vue';

import { type Message } from '@/entities/Message';

import TextMessageView from './TextMessageView.vue';
import RollMessageView from './RollMessageView.vue';
import SkillRollMessageView from './SkillRollMessageView.vue';
import { useSession } from '@/composables/useSession';

const { message } = defineProps<{
    message: Message;
}>();

const session = useSession();
const isUserSender = computed(() => message.sender.id === session.userId.value);

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

<template>
    <div class="pb-2 w-full">
        <component :is="getComponentByType(message)" :message="message" :align-right="isUserSender">
            Unknown message type: {{ message.$type }}
        </component>
    </div>
</template>

<style scoped></style>
