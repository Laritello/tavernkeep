<script setup lang="ts">
import { ref, watch } from 'vue';
import { useRouter } from 'vue-router';

import CharacterHub from '@/api/hubs/CharacterHub.ts';
import ChatHub from '@/api/hubs/ChatHub.ts';
import { useAuth } from '@/composables/useAuth.ts';
import { useSession } from '@/composables/useSession.ts';
import { useTheme } from '@/composables/useTheme.ts';
import { useI18n } from '@/i18n/useI18n.ts';
import { useCharacters } from '@/stores/characters.ts';
import { useMessages } from '@/stores/messages.ts';
import { useUsers } from '@/stores/users.ts';

import EncounterHub from './api/hubs/EncounterHub';

const router = useRouter();
const session = useSession();
const users = useUsers();
const characters = useCharacters();
const messages = useMessages();

const isLoading = ref(false);

useTheme(); // apply theme
useI18n(true); // apply locale
initialize().catch(console.error);

watch(session.isAuthenticated, async (value) => {
    if (!value) {
        console.info('[AppLoader] User logged out');
        console.info('[AppLoader] Stop hubs and go to login page');

        await stopHubs();
        await router.push('/login');
        return;
    } else {
        console.info('[AppLoader] User logged in. Fetching data...');
        await initialize();
    }
});

async function initialize() {
    if (!session.isAuthenticated.value) {
        return;
    }

    console.groupCollapsed('[AppLoader] Initialize');
    isLoading.value = true;
    try {
        // await new Promise((resolve) => setTimeout(resolve, 1500));
        await fetch();
        console.info('[AppLoader] Initial data fetched');

        await startHubs();
        console.info('[AppLoader] Hubs started');
        console.info('[AppLoader] Initialized');
        isLoading.value = false;
    } catch (e) {
        console.warn('[AppLoader] Something went wrong. Logout now!');
        isLoading.value = false;
        const auth = useAuth();
        auth.logout();
        throw e;
    } finally {
        console.groupEnd();
    }
}

async function fetch() {
    await users.fetch();
    await characters.fetch();
    await messages.fetch(0, 20);
}

async function startHubs() {
    await ChatHub.start();
    await CharacterHub.start();
    await EncounterHub.start();
}

async function stopHubs() {
    return Promise.all([ChatHub.stop(), CharacterHub.stop(), EncounterHub.stop()]);
}
</script>

<template>
    <Transition name="fade" mode="in-out">
        <div
            v-if="isLoading"
            class="fixed flex z-50 flex-col bg-base-100 h-screen w-screen items-center justify-center"
        >
            <h1 class="flex align-text-bottom text-5xl">
                Loading
                <span class="loading loading-dots loading-md relative top-4"></span>
            </h1>
        </div>
    </Transition>
    <slot />
</template>

<style scoped>
.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.35s ease;
}

.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}
</style>
