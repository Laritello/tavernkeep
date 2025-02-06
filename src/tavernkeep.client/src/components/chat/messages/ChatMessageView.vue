<script setup lang="ts">
import { computed } from 'vue';

import { useSession } from '@/composables/useSession';
import { RollMessageType } from '@/contracts/enums';
import { type Message, type SkillRollMessage } from '@/entities/Message';

import RollMessageView from './RollMessageView.vue';
import TextMessageView from './TextMessageView.vue';

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
                type: RollMessageType.Custom,
                subHeader: 'Roll',
            };
        case 'SkillRollMessage':
            return {
                type: RollMessageType.Skill,
                subHeader: (message as SkillRollMessage).skill.name,
                skillType: (message as SkillRollMessage).skill.type,
            };
        default:
            return {};
    }
}
</script>

<template>
    <div class="pb-2 w-full">
        <component
            :is="getComponentByType(message)"
            :message="message"
            :align-right="isUserSender"
            :roll-message-parameters="getParametersByType(message)"
        >
            Unknown message type: {{ message.$type }}
        </component>
    </div>
</template>

<style scoped></style>
