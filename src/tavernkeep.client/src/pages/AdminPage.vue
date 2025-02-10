<script setup lang="ts">
import { reactive } from 'vue';

import UserSelector from '@/components/chat/UserSelector.vue';
import ConfirmationDialog from '@/components/dialogs/ConfirmationDialog.vue';
import { useModal } from '@/composables/useModal';
import { UserRole } from '@/contracts/enums/UserRole';
import { useCharacters } from '@/stores/characters';
import { useUsers } from '@/stores/users';

const users = useUsers();
const characters = useCharacters();

const newCharacterModel = reactive({
    userId: '',
    characterName: '',
});

const newUserModel = reactive({
    login: '',
    password: '',
    role: UserRole.Player,
});

async function createUser() {
    await users.createUser(newUserModel.login, newUserModel.password, newUserModel.role);
    newUserModel.login = '';
    newUserModel.password = '';
}

async function createCharacter() {
    await characters.createCharacter(newCharacterModel.userId, newCharacterModel.characterName);
    newCharacterModel.userId = '';
    newCharacterModel.characterName = '';
}
async function setActiveCharacter(userId: string, characterId: string) {
    await users.setActiveCharacter(userId, characterId);
}

async function assign(characterId: string, userId: string) {
    await characters.assignUserToCharacter(userId, characterId);
}

async function deleteCharacter(id: string) {
    const modal = useModal();
    const result = await modal.show(ConfirmationDialog, {
        caption: 'Delete character',
        message: 'Are you sure you want to delete this character?',
    });
    if (result.action !== 'confirm') return;
    await characters.deleteCharacter(id);
}

async function deleteUser(id: string) {
    const modal = useModal();
    const result = await modal.show(ConfirmationDialog, {
        caption: 'Delete user',
        message: 'Are you sure you want to delete this user?',
    });
    if (result.action !== 'confirm') return;
    await users.deleteUser(id);
}
</script>

<template>
    <div class="space-y-4 px-2 py-4 h-full overflow-auto">
        <div class="space-y-2 bg-base-300 shadow shadow-gray-950 rounded p-2">
            <div class="text-lg">Create character</div>
            <form class="flex space-x-2" @submit.prevent="createCharacter">
                <label class="input input-bordered flex min-w-64 items-center gap-2">
                    <!-- prettier-ignore -->
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="w-4 h-4 opacity-70" ><path d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6ZM12.735 14c.618 0 1.093-.561.872-1.139a6.002 6.002 0 0 0-11.215 0c-.22.578.254 1.139.872 1.139h9.47Z" /></svg>
                    <input
                        v-model="newCharacterModel.characterName"
                        type="text"
                        placeholder="Character name"
                        required
                    />
                </label>
                <UserSelector
                    v-model="newCharacterModel.userId"
                    :users="Object.values(users.dictionary)"
                    class="pr-3"
                />
                <input type="submit" value="Create new" class="btn btn-active justify-end" />
            </form>
        </div>
        <form class="space-y-2 bg-base-300 shadow shadow-gray-950 rounded p-2" @submit.prevent="createUser">
            <div class="text-lg">Create user</div>
            <label class="input input-bordered flex items-center gap-2">
                <!-- prettier-ignore -->
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="w-4 h-4 opacity-70" ><path d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6ZM12.735 14c.618 0 1.093-.561.872-1.139a6.002 6.002 0 0 0-11.215 0c-.22.578.254 1.139.872 1.139h9.47Z" /></svg>
                <input
                    v-model="newUserModel.login"
                    type="text"
                    class="grow"
                    placeholder="Username"
                    autocomplete="off"
                    required
                />
            </label>
            <label class="input input-bordered flex items-center gap-2">
                <!-- prettier-ignore -->
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="w-4 h-4 opacity-70" ><path fill-rule="evenodd" d="M14 6a4 4 0 0 1-4.899 3.899l-1.955 1.955a.5.5 0 0 1-.353.146H5v1.5a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1-.5-.5v-2.293a.5.5 0 0 1 .146-.353l3.955-3.955A4 4 0 1 1 14 6Zm-4-2a.75.75 0 0 0 0 1.5.5.5 0 0 1 .5.5.75.75 0 0 0 1.5 0 2 2 0 0 0-2-2Z" clip-rule="evenodd" /></svg>
                <input
                    v-model="newUserModel.password"
                    type="password"
                    class="grow"
                    placeholder="Password"
                    autocomplete="off"
                    required
                />
            </label>
            <v-select
                v-model="newUserModel.role"
                label="Role"
                :items="[UserRole.Master, UserRole.Moderator, UserRole.Player]"
            />
            <button type="submit" class="btn w-full btn-active">Create new</button>
        </form>
        <div v-for="user in users.dictionary" :key="user.id" class="bg-base-300 shadow shadow-gray-950 rounded p-2">
            <div>
                <div class="flex gap-2 text-lg font-bold border-b">
                    <div>{{ user.role }}</div>
                    <div>{{ user.login }}</div>
                    <div class="flex flex-1 justify-end">
                        <v-btn
                            size="small"
                            variant="text"
                            title="Delete user"
                            icon="mdi-delete"
                            @click="deleteUser(user.id)"
                        />
                    </div>
                </div>
                <div>
                    <div
                        v-for="(character, i) in Object.values(characters.all).filter((c) => c.ownerId === user.id)"
                        :key="character.id"
                        class="flex items-center px-2 py-3 my-2 space-x-4"
                        :class="{ 'active-character': character.id === user.activeCharacterId }"
                    >
                        <div>{{ i + 1 }}</div>
                        <div class="w-24">{{ character.name }}</div>
                        <div class="flex items-center flex-1 justify-end">
                            <UserSelector
                                v-model="character.ownerId"
                                :users="Object.values(users.dictionary)"
                                class="pr-3"
                                @update:model-value="(userId) => assign(character.id, userId)"
                            />
                            <button
                                :disabled="character.id === user.activeCharacterId"
                                class="btn btn-sm btn-active"
                                @click="setActiveCharacter(user.id, character.id)"
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
            </div>
        </div>
    </div>
</template>

<style scoped>
.active-character {
    @apply bg-base-200 rounded shadow-md;
}
</style>
