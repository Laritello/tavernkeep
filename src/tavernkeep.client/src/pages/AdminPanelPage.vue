<template>
    <div class="container m-4">
        <h1 class="text-xl">GM Panel</h1>
        <div class="w-64 flex flex-col gap-4">
            <v-card>
                <UserForm />
            </v-card>
            <v-card>
                <UserList />
            </v-card>
            <v-card>
                <v-sheet class="mx-auto">
                    <v-form fast-fail @submit.prevent="charactersStore.createCharacter(characterName)" class="pa-2">
                        <div class="text-h6">Create character</div>
                        <v-text-field v-model="characterName" label="Login"></v-text-field>
                        <v-btn type="submit" block>Create</v-btn>
                    </v-form>
                </v-sheet>
            </v-card>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

import { useCharactersStore } from '@/stores/characters.store';

import UserForm from '@/components/UserForm.vue';
import UserList from '@/components/UserList.vue';

const charactersStore = useCharactersStore();
const characterName = ref('');
</script>
