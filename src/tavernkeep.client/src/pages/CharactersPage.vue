<template>
    <div class="container m-4">
        <h1 class="text-xl">Characters</h1>
        <div class="w-64 flex flex-col gap-4">
            <v-form fast-fail @submit.prevent="charactersStore.createCharacter(characterName)" class="pa-2">
                <v-text-field v-model="characterName" label="Character name"></v-text-field>
                <v-btn type="submit" block>Create new</v-btn>
            </v-form>
        </div>
        <div class="w-full p-4 rounded shadow">
            <template v-for="character in charactersStore.characters" :key="character.id">
                <div class="flex gap-2">
                    <span class="w-20">{{ character.name }}</span>
                    <span class="w-20">{{ character.owner.login || 'Unknown' }}</span>
                    <UserSelector v-model="character.owner.id" :users="usersStore.users" />
                    <v-btn @click="assign(character.id, character.owner.id)"> Assign </v-btn>
                    <v-btn
                        size="small"
                        variant="text"
                        icon="mdi-delete"
                        @click="charactersStore.deleteCharacter(character.id)"
                    />
                </div>
            </template>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

import { useCharactersStore } from '@/stores/characters.store';
import { useUsersStore } from '@/stores/users.store';
import UserSelector from '@/components/chat/UserSelector.vue';

const usersStore = useUsersStore();
const charactersStore = useCharactersStore();

const characterName = ref('');

async function assign(characterId: string, userId: string) {
    const newCharacter = await charactersStore.assignUserToCharacter(userId, characterId);
    console.log(newCharacter);
}
</script>
