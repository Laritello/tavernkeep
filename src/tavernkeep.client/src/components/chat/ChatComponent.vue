<script setup lang="ts">
import { ref, computed, reactive } from 'vue';
import { RollType } from '@/contracts/enums/RollType';
import { useMessages } from '@/stores/messages';
import { useUsers } from '@/stores/users';
import { useSession } from '@/composables/useSession';
import { useModal } from '@/composables/useModal';

// Components
import ChatMessageView from './messages/ChatMessageView.vue';
import ChatInputView from './ChatInputView.vue';
import { ContextMenu, ContextMenuItem, ContextMenuSeparator } from '@imengyu/vue3-context-menu'
import ConfirmationDialog from '@/components/dialogs/ConfirmationDialog.vue';
import { UserRole } from '@/contracts/enums';

const session = useSession();
const messages = useMessages();

const message = ref('');
const selectedUserId = ref<string>();
const contextMenuShown = ref(false);
const contextMenuOptions = reactive({
    zIndex: 3,
    minWidth: 230,
    x: 0,
    y: 0,
    messageId: '',
    theme: 'default ' + localStorage.getItem("theme"),
});

// TODO: Restore this feature
const listOfMessageRecepient = computed(() => {
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

function onContextMenu(e : MouseEvent, messageId: string) {
    contextMenuShown.value = true;
    contextMenuOptions.x = e.clientX;
    contextMenuOptions.y = e.clientY;
    contextMenuOptions.messageId = messageId;
}

async function deleteMessage(messageId: string) {
    const modal = useModal();
    const result = await modal.show(ConfirmationDialog, {
        caption: 'Delete message',
        message: 'Are you sure you want to delete this message?',
    });
    if (result.action !== 'confirm') return;
    await messages.deleteMessage(messageId);
}

function havePermissionToDelete() {
    return session.havePermissions([UserRole.Master, UserRole.Moderator])
}

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
        if (message.value.length == 0) {
            return;
        }

        await messages.createMessage(message.value, privateMessageRecipient);
    }

    message.value = '';
}
</script>

<template>
    <div class="flex flex-col h-full">
        <div v-chat-scroll="{ always: false, smooth: true }" class="w-full grow max-h-full overflow-y-auto px-2">
            <ChatMessageView 
                v-for="item in messages.list" :key="item.id" 
                :message="item" 
                @contextmenu.prevent="onContextMenu($event, item.id)" />
        </div>

        <form @submit.prevent="sendMessage" class="flex flex-row mb-1">
            <ChatInputView v-model:content="message" :commands="slashCommands" class="w-full" />
            <div class="p-1">
                <button type="submit" class="btn btn-md btn-circle btn-primary text-white shadow-lg dark:text-neutral mt-1">
                    <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" viewBox="0 -960 960 960"
                        fill="currentColor">
                        <path
                            d="M120-160v-640l760 320-760 320Zm80-120 474-200-474-200v140l240 60-240 60v140Zm0 0v-400 400Z" />
                    </svg>
                </button>
            </div>
        </form>
    </div>
    <ContextMenu
        v-model:show="contextMenuShown"
        :options="contextMenuOptions"
    >
        <!-- TODO: add reply feature -->
        <ContextMenuItem label="Reply" @click="console.log('Reply item click')" />
        <ContextMenuSeparator 
            v-if="havePermissionToDelete()" />
        <ContextMenuItem 
            v-if="havePermissionToDelete()" 
            label="Delete" 
            @click="deleteMessage(contextMenuOptions.messageId)">
            <template #icon>
                <span class="mdi mdi-delete text-lg text-red-600"></span>
            </template>
        </ContextMenuItem>
    </ContextMenu>
</template>

<style scoped></style>
