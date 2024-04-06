<template>
    <div v-if="user" class="space-y-4 px-2 py-4 h-full overflow-auto">
        <div class="bg-base-300 shadow shadow-gray-950 rounded p-2">
            <h1 class="text-xl">Characters</h1>
            <div
                v-for="(character, i) in appStore.characters.mapByUserId.get(user.id) || []"
                :key="character.id"
                class="flex items-center rounded px-2 py-3 my-2 space-x-4 hover:bg-base-200"
                :class="{ 'active-character': character.id === user.activeCharacterId }"
            >
                <div>{{ i + 1 }}</div>
                <div class="w-24">{{ character.name }}</div>
                <div class="flex items-center flex-1 justify-end">
                    <button
                        @click="setActiveCharacter(character.ownerId, character.id)"
                        :disabled="character.id === user.activeCharacterId"
                        class="btn btn-sm btn-active"
                    >
                        Set active
                    </button>
                    <v-btn
                        size="small"
                        variant="text"
                        icon="mdi-delete"
                        :disabled="character.id === user.activeCharacterId"
                        @click="deleteCharacter(character.id)"
                    />
                </div>
            </div>
        </div>
        <div class="space-y-2 bg-base-300 shadow shadow-gray-950 rounded p-2">
            <div class="text-lg">Create character</div>
            <form @submit.prevent="createCharacter(user.id, characterNameRef)" class="flex space-x-2">
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
<script setup lang="ts">
import { useAppStore } from '@/stores/app.store';
import { ref } from 'vue';
import { storeToRefs } from 'pinia';
import { useModal } from '@/composables/useModal';
import ConfirmationDialog from '@/components/dialogs/ConfirmationDialog.vue';

const modal = useModal();
const appStore = useAppStore();
const { currentUser: user } = storeToRefs(appStore.users);

const characterNameRef = ref('');

async function deleteCharacter(id: string) {
    const result = await modal.show(ConfirmationDialog, {
        caption: 'Delete character',
        message: 'Are you sure you want to delete this character?',
    });
    if (result.action !== 'confirm') return;
    appStore.characters.deleteCharacter(id);
}

async function createCharacter(userId: string, characterName: string) {
    await appStore.characters.createCharacter(userId, characterName);
    characterNameRef.value = '';
}
async function setActiveCharacter(userId: string, characterId: string) {
    await appStore.users.setActiveCharacter(userId, characterId);
}
</script>
<style scoped>
.active-character {
    @apply bg-base-200 shadow-md;
}
</style>
