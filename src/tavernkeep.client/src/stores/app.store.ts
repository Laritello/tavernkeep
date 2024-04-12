import { watch } from 'vue';
import { defineStore } from 'pinia';

import ChatHub from '@/api/hubs/ChatHub';
import CharacterHub from '@/api/hubs/CharacterHub';

import { useUsersStore } from './users';
import { useCharactersStore } from './characters.store';
import { useMessagesStore } from './messages.store';
import { useSession } from '@/composables/useSession';

export const useAppStore = defineStore('app.store', () => {
    const session = useSession();
    const users = useUsersStore();
    const characters = useCharactersStore();
    const messages = useMessagesStore();

    console.groupCollapsed('[AppStore] Initialize');
    if (session.isAuthenticated.value) {
        console.info('[AppStore] User already logged in. Fetching data...');
        initialize().then(() => {
            console.info('[AppStore] Initialized');
            console.groupEnd();
        });
    }

    watch(session.isAuthenticated, (value): void => {
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

    return { users, characters, messages };
});
