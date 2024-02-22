<template>
    <!-- <v-layout class="rounded-md">
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
    </v-layout> -->
    <div class="flex flex-col h-screen">
        <!-- Navbar -->
        <div class="relative navbar bg-primary text-primary-content z-[1] shadow-sm shadow-neutral-950">
            <div class="flex-1">
                <h1 class="text-xl px-4 cursor-default">Tavernkeep</h1>
            </div>
            <label class="flex cursor-pointer gap-2 mr-4">
                <span class="label-text text-primary-content">Light</span>
                <input type="checkbox" value="dark" class="toggle theme-controller" />
                <span class="label-text text-primary-content">Dark</span>
            </label>
            <div class="flex-none text-base-content">
                <span class="text-primary-content">{{ auth.userName }}</span>
                <div class="dropdown dropdown-end">
                    <div tabindex="0" role="button" class="btn btn-ghost btn-circle avatar">
                        <div class="avatar w-10 rounded-full bg-orange-400"></div>
                    </div>
                    <ul
                        tabindex="0"
                        class="menu menu-sm dropdown-content mt-3 z-[1] p-2 shadow bg-base-100 rounded-md w-52"
                    >
                        <li><a>Settings</a></li>
                        <li><a @click.prevent="logout">Logout</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="flex flex-row content-height">
            <!-- Left panel -->
            <div class="flex flex-col gap-4 w-72 shadow-md shadow-neutral-950 bg-base-200 p-4">
                <h1 class="text-xl">Sidebar</h1>
                <v-card>
                    <UserForm />
                </v-card>
                <v-card>
                    <UserList />
                </v-card>
            </div>
            <!-- Content -->
            <main class="flex grow bg-base">
                <div class="container m-4">
                    <PartyComponent />
                </div>
            </main>
            <!-- Chat panel -->
            <div class="w-96 shadow-md shadow-neutral-950 bg-base-200">
                <ChatComponent class="flex" />
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import UserForm from '@/components/UserForm.vue';
import UserList from '@/components/UserList.vue';
import ChatComponent from '@/components/ChatComponent.vue';
import { useUsersStore } from '@/stores/users.store';
import PartyComponent from './PartyComponent.vue';
import { useCharactersStore } from '@/stores/characters.store';
import { useAuthStore } from '@/stores/auth.store';
import { useRouter } from 'vue-router';

const usersStore = useUsersStore();
const charactersStore = useCharactersStore();
const auth = useAuthStore();
const router = useRouter();

async function logout() {
    auth.logout();
    router.push('/login');
}

onMounted(async () => {
    await usersStore.fetchUsers();
    await charactersStore.fetchCharacters();
});
</script>

<style scoped>
.content-height {
    height: calc(100% - 4rem);
}
</style>
