<script setup lang="ts">
import { computed } from 'vue';

import { type Message, type SavingThrowRollMessage, type SkillRollMessage } from '@/entities/Message';

import TextMessageView from './TextMessageView.vue';
import RollMessageView from './RollMessageView.vue';
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
        case 'SkillRollMessage':
        case 'SavingThrowRollMessage':
            return RollMessageView;
        default:
            return `div`;
    }
}

function getParametersByType(message: Message) {
    switch (message.$type) {
        case 'RollMessage':
            return {
                header: 'Custom',
                subHeader: 'Roll'
            };
        case 'SkillRollMessage':
            return {
                header: 'Check',
                subHeader: (message as SkillRollMessage).skill.type
            };
        case 'SavingThrowRollMessage':
            return {
                header: 'Save',
                subHeader: (message as SavingThrowRollMessage).savingThrow.type
            };
        default:
            return {};
    }
}
</script>

<template>
    <div class="pb-2 w-full">
        <component :is="getComponentByType(message)" :message="message" :align-right="isUserSender"
            :rollMessageParameters="getParametersByType(message)">
            Unknown message type: {{ message.$type }}
        </component>
    </div>
</template>

<style scoped></style>
