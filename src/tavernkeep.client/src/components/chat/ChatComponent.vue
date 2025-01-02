<script setup lang="ts">
import { computed, ref } from 'vue';
import { RollType } from '@/contracts/enums/RollType';

// Components
import UserSelector from './UserSelector.vue';
import ChatMessageView from './messages/ChatMessageView.vue';
import CommandInput from './CommandInput.vue';
import { useMessages } from '@/stores/messages';
import { useUsers } from '@/stores/users';
import { useSession } from '@/composables/useSession';

const messages = useMessages();

const message = ref('');
const selectedUserId = ref<string>();

const listOfMessageRecepient = computed(() => {
    const session = useSession();
    const users = useUsers();
    if (!session.isAuthenticated) return [];

    const currentUserId = session.userId.value!;
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    const { [currentUserId]: _, ...rest } = users.dictionary;
    return Object.values(rest);
});

const slashCommands = [
    {
        name: '/roll',
        description: 'Make public roll in chat',
    },
    {
        name: '/sroll',
        description: 'Same as /roll but result will be seen only by GM',
    },
    {
        name: '/proll',
        description: 'Same as /roll but result will be seen only by GM and you',
    },
];

async function sendMessage() {
    if (message.value.startsWith('/')) {
        const [command, expression] = message.value.split(' ', 2);
        let rollType = RollType.Public;
        switch (command) {
            case '/roll':
                break;
            case '/sroll':
                rollType = RollType.Secret;
                break;
            case '/proll':
                rollType = RollType.Private;
                break;
            default:
                console.warn('Unknown command');
        }
        await messages.createRollMessage(expression, rollType);
    } else {
        const privateMessageRecipient = selectedUserId.value || undefined;
        await messages.createMessage(message.value, privateMessageRecipient);
    }

    message.value = '';
}
</script>

<template>
    <div class="flex flex-col h-full">
        <div v-chat-scroll="{ always: false, smooth: true }" class="w-full grow max-h-full overflow-y-auto px-2 mb-2">
            <ChatMessageView v-for="item in messages.list" :key="item.id" :message="item" />
        </div>
        <div class="border-t-2 py-2">
            <form @submit.prevent="sendMessage" class="flex flex-row gap-2 mx-2">
                <CommandInput v-model="message" :commands="slashCommands" class="w-full" />
                <button type="submit" class="btn btn-circle btn-primary text-white dark:text-neutral">
                    <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" viewBox="0 -960 960 960"
                        fill="currentColor">
                        <path
                            d="M120-160v-640l760 320-760 320Zm80-120 474-200-474-200v140l240 60-240 60v140Zm0 0v-400 400Z" />
                    </svg>
                </button>
            </form>
        </div>

    </div>
</template>

<style scoped></style>
