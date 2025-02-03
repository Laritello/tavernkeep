<script setup lang="ts">
import { ref } from 'vue';

import ConfirmationDialog from '@/components/dialogs/ConfirmationDialog.vue';
import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';
import { useModal } from '@/composables/useModal';

const userAccount = useCurrentUserAccount();

const currentUser = userAccount.user;
const characterNameRef = ref('');

async function createCharacter(characterName: string) {
    await userAccount.createCharacter(characterName);
    characterNameRef.value = '';
}

async function deleteCharacter(id: string) {
    const modal = useModal();
    const result = await modal.show(ConfirmationDialog, {
        caption: 'Delete character',
        message: 'Are you sure you want to delete this character?',
    });
    if (result.action !== 'confirm') return;
    await userAccount.deleteCharacter(id);
}

async function setActiveCharacter(characterId: string) {
    await userAccount.setActiveCharacter(characterId);
}

const isActiveCharacter = (id: string) => id === currentUser.value?.activeCharacterId;
</script>

<template>
    <div v-if="currentUser" class="space-y-4 px-2 py-4 h-full overflow-auto">
        <div class="bg-base-300 shadow shadow-gray-950 rounded p-2">
            <h1 class="text-xl">Characters</h1>
            <div
                v-for="(character, i) in userAccount.characters.value"
                :key="character.id"
                class="flex items-center rounded px-2 py-3 my-2 space-x-4 hover:bg-base-200"
                :class="{ 'active-character': isActiveCharacter(character.id) }"
            >
                <div>{{ i + 1 }}</div>
                <div class="w-24">{{ character.name }}</div>
                <div class="flex items-center flex-1 justify-end">
                    <button
                        @click="setActiveCharacter(character.id)"
                        :disabled="isActiveCharacter(character.id)"
                        class="btn btn-sm btn-active"
                    >
                        Set active
                    </button>
                    <button
                        class="btn btn-sm btn-square btn-outline"
                        :disabled="isActiveCharacter(character.id)"
                        @click="deleteCharacter(character.id)"
                    >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            class="h-6 w-6"
                            fill="none"
                            viewBox="0 0 24 24"
                            stroke="currentColor"
                        >
                            <path
                                stroke-linecap="round"
                                stroke-linejoin="round"
                                stroke-width="2"
                                d="M6 18L18 6M6 6l12 12"
                            />
                        </svg>
                    </button>
                </div>
            </div>
        </div>
    </div>
    <div class="space-y-4 px-2 py-4 h-full overflow-auto">
        <div class="space-y-2 bg-base-300 shadow shadow-gray-950 rounded p-2">
            <div class="text-lg">Create character</div>
            <form @submit.prevent="createCharacter(characterNameRef)" class="flex space-x-2">
                <label class="input input-bordered flex grow items-center gap-2">
                    <!-- prettier-ignore -->
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="w-4 h-4 opacity-70" ><path d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6ZM12.735 14c.618 0 1.093-.561.872-1.139a6.002 6.002 0 0 0-11.215 0c-.22.578.254 1.139.872 1.139h9.47Z" /></svg>
                    <input v-model="characterNameRef" type="text" placeholder="Character name" required />
                </label>
                <input type="submit" value="Create new" class="btn btn-active justify-end" />
            </form>
        </div>
    </div>
</template>

<style scoped>
.active-character {
    @apply bg-base-200 shadow-md;
}
</style>
