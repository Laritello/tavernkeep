<template>
    <v-layout class="rounded-md">
        <v-app-bar color="primary" title="Tavernkeep"></v-app-bar>

        <v-navigation-drawer>
            <v-col>
                <v-card>
                    <UserForm />
                </v-card>
                <v-card class="mt-3">
                    <UserList />
                </v-card>
            </v-col>
        </v-navigation-drawer>

        <v-navigation-drawer location="right" width="400" permanent>
            <ChatComponent class="max-h-full"> </ChatComponent>
        </v-navigation-drawer>

        <v-main class="flex align-center justify-center min-h-80">
            <PartyComponent />
        </v-main>
    </v-layout>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import UserForm from '@/components/UserForm.vue';
import UserList from '@/components/UserList.vue';
import ChatComponent from '@/components/ChatComponent.vue';
import { useUsersStore } from '@/stores/users.store';
import PartyComponent from './PartyComponent.vue';
import { useCharactersStore } from '@/stores/characters.store';

const usersStore = useUsersStore();
const charactersStore = useCharactersStore();

onMounted(async () => {
    await usersStore.fetchUsers();
    await charactersStore.fetchCharacters();
});
</script>
@/stores/users.store
