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
    <div class="flex flex-col">
        <h1 class="text-xl p-2">Chat</h1>
        <div class="flex grow overflow-auto" v-chat-scroll="{ always: false, smooth: true }">
            <div class="w-full px-4">
                <ChatMessageView v-for="item in messages.list" :key="item.id" :message="item" />
            </div>
        </div>
        <div class="w-full absolute bottom-0 pr-8">
            <UserSelector v-model="selectedUserId" :users="listOfMessageRecepient" />
            <form @submit.prevent="sendMessage">
                <div class="flex gap-2 my-2">
                    <CommandInput v-model="message" :commands="slashCommands" class="w-full" />
                    <button type="submit" class="btn btn-ghost">
                        <v-icon icon="mdi-send" />
                    </button>
                </div>
            </form>
        </div>
    </div>
</template>

<style scoped></style>
