import { watch } from 'vue';
import { defineStore, storeToRefs } from 'pinia';

import { useAuthStore } from './auth.store';
import { useUsersStore } from './users.store';
import { useCharactersStore } from './characters.store';
import { useMessagesStore } from './messages.store';

import ChatHub from '@/api/hubs/ChatHub';
import CharacterHub from '@/api/hubs/CharacterHub';

export const useAppStore = defineStore('app.store', () => {
    const auth = useAuthStore();
    const users = useUsersStore();
    const characters = useCharactersStore();
    const messages = useMessagesStore();

    const { isLoggedIn } = storeToRefs(auth);

    console.groupCollapsed('[AppStore] Initialize');
    if (isLoggedIn.value) {
        console.info('[AppStore] User already logged in. Fetching data...');
        initialize().then(() => {
            console.info('[AppStore] Initialized');
            console.groupEnd();
        });
    }

    watch(isLoggedIn, (value): void => {
        console.info('[AppStore] Auth status update');
        if (!value) {
            console.info('[AppStore] User logged out');
            stopHubs().then(() => console.info('[AppStore] Hubs stopped'));
            return;
        }
        console.info('[AppStore] User logged in. Fetching data...');
        initialize().then(() => console.info('[AppStore] reInitialized'));
    });

    async function fetch(): Promise<void[]> {
        return Promise.all([users.fetch(), characters.fetch(), messages.fetch(0, 20)]);
    }

    async function initialize() {
        const fetchPromise = fetch().then(() => console.info('[AppStore] Initial data fetched'));
        const startHubsPromise = startHubs().then(() => console.info('[AppStore] Hubs started'));
        return Promise.all([fetchPromise, startHubsPromise]);
    }

    async function startHubs() {
        return Promise.all([ChatHub.start(), CharacterHub.start()]);
    }

    async function stopHubs() {
        return Promise.all([ChatHub.stop(), CharacterHub.stop()]);
    }

    return { auth, users, characters, messages };
});
