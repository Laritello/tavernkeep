<script setup lang="ts">
import { ContextMenu, ContextMenuItem, ContextMenuSeparator } from '@imengyu/vue3-context-menu';
import { useClipboard } from '@vueuse/core';
import { ref, reactive } from 'vue';

import ConfirmationDialog from '@/components/dialogs/ConfirmationDialog.vue';
import DiceRollerButton from '@/components/shared/DiceRoller/DiceRollerButton.vue';
import DiceRollerMenu from '@/components/shared/DiceRoller/DiceRollerMenu.vue';
import { useModal } from '@/composables/useModal';
import { useSession } from '@/composables/useSession';
import { UserRole } from '@/contracts/enums';
import { RollType } from '@/contracts/enums/RollType';
import type { Message, TextMessage } from '@/entities';
import { useMessages } from '@/stores/messages';

import ChatInputView from './ChatInputView.vue';
// Components
import ChatMessageView from './messages/ChatMessageView.vue';

const session = useSession();
const messages = useMessages();

const diceRollerMenuRef = ref<InstanceType<typeof DiceRollerMenu>>();
const message = ref('');
const selectedUserId = ref<string>();
const contextMenuShown = ref(false);
const contextMenuOptions = reactive({
    zIndex: 3,
    minWidth: 230,
    x: 0,
    y: 0,
    message: undefined as Message | undefined,
    theme: 'default ' + localStorage.getItem('theme'),
});

// TODO: Restore this feature
// const listOfMessageRecepient = computed(() => {
//     const users = useUsers();
//     if (!session.isAuthenticated) return [];
//
//     const currentUserId = session.userId.value!;
//     // eslint-disable-next-line @typescript-eslint/no-unused-vars
//     const { [currentUserId]: _, ...rest } = users.dictionary;
//     return Object.values(rest);
// });

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

function onContextMenu(e: MouseEvent, message: Message) {
    contextMenuShown.value = true;
    contextMenuOptions.x = e.clientX;
    contextMenuOptions.y = e.clientY;
    contextMenuOptions.message = message;
}

function copyMessageText() {
    const { copy } = useClipboard();
    if (contextMenuOptions.message!.$type === 'TextMessage') {
        const message = contextMenuOptions.message! as TextMessage;
        copy(message.text);
    }
}

async function deleteMessage() {
    const modal = useModal();
    const result = await modal.show(ConfirmationDialog, {
        caption: 'Delete message',
        message: 'Are you sure you want to delete this message?',
    });
    if (result.action !== 'confirm') return;
    const message = contextMenuOptions.message!;
    await messages.deleteMessage(message.id);
}

function havePermissionToDelete() {
    return session.havePermissions([UserRole.Master, UserRole.Moderator]);
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
                v-for="item in messages.list"
                :key="item.id"
                :message="item"
                @contextmenu.prevent="onContextMenu($event, item)"
                class="select-none"
            />
        </div>

        <form @submit.prevent="sendMessage" class="flex flex-row mb-1">
            <ChatInputView v-model:content="message" :commands="slashCommands" class="w-full" />
            <div class="p-1">
                <button v-if="!!message.trim()" type="submit" class="btn btn-md btn-circle btn-primary shadow-lg mt-1">
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        class="w-6 h-6"
                        viewBox="0 -960 960 960"
                        fill="currentColor"
                    >
                        <path
                            d="M120-160v-640l760 320-760 320Zm80-120 474-200-474-200v140l240 60-240 60v140Zm0 0v-400 400Z"
                        />
                    </svg>
                </button>
                <DiceRollerButton
                    v-else
                    @click="diceRollerMenuRef?.open()"
                    class="btn btn-md btn-circle btn-primary shadow-lg mt-1"
                />
            </div>
        </form>
    </div>
    <ContextMenu v-model:show="contextMenuShown" :options="contextMenuOptions">
        <!-- TODO: add reply feature -->
        <!--<ContextMenuItem label="Reply" @click="console.log('Reply item click')" />-->
        <ContextMenuItem
            v-if="contextMenuOptions.message?.$type === 'TextMessage'"
            label="Copy"
            @click="copyMessageText()"
        />
        <ContextMenuSeparator v-if="havePermissionToDelete()" />
        <ContextMenuItem
            v-if="havePermissionToDelete()"
            label="Delete"
            icon="mdi mdi-delete text-lg text-red-600"
            @click="deleteMessage()"
        >
        </ContextMenuItem>
    </ContextMenu>
    <DiceRollerMenu ref="diceRollerMenuRef" />
</template>

<style scoped></style>
