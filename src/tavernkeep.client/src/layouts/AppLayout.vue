<template>
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
                        <li>
                            <a @click.prevent="logout">Logout</a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="flex flex-row content-height">
            <!-- Left panel -->
            <div class="flex flex-col gap-4 w-72 shadow-md shadow-neutral-950 bg-base-200 p-4">
                <h1 class="text-xl">Sidebar</h1>
                <ul class="menu menu-sm">
                    <li>
                        <RouterLink to="/">Home</RouterLink>
                    </li>
                    <li v-if="auth.role == UserRole.Master">
                        <RouterLink to="/admin">Admin Panel</RouterLink>
                    </li>
                </ul>
            </div>
            <!-- Content -->
            <main class="flex grow bg-base">
                <div class="container m-4">
                    <slot />
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
import { useRouter } from 'vue-router';
import { UserRole } from '@/contracts/enums/UserRole';

// Components
import ChatComponent from '@/components/chat/ChatComponent.vue';

// Stores
import { useCharactersStore } from '@/stores/characters.store';
import { useMessagesStore } from '@/stores/messages.store';
import { useUsersStore } from '@/stores/users.store';
import { useAuthStore } from '@/stores/auth.store';

const charactersStore = useCharactersStore();
const messagesStore = useMessagesStore();
const usersStore = useUsersStore();
const auth = useAuthStore();

// Fetch data from server
charactersStore.fetchCharacters();
messagesStore.fetchMessages(0, 20);
usersStore.fetchUsers();

const router = useRouter();

async function logout() {
    auth.logout();
    router.push('/login');
}
</script>
<style scoped>
.content-height {
    height: calc(100% - 4rem);
}
</style>
