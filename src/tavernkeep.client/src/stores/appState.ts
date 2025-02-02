import { defineStore } from 'pinia';
import { ref, watch } from 'vue';

import CharacterHub from '@/api/hubs/CharacterHub';
import ChatHub from '@/api/hubs/ChatHub';
import { useAuth } from '@/composables/useAuth';
import { useSession } from '@/composables/useSession';
import { useI18n } from '@/i18n/useI18n.ts';
import { useCharacters } from '@/stores/characters';
import { useMessages } from '@/stores/messages';
import { useUsers } from '@/stores/users';

export const useAppState = defineStore('appState', () => {
    const session = useSession();
    const users = useUsers();
    const characters = useCharacters();
    const messages = useMessages();
    const isLoading = ref(true);

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
        // apply locale
        useI18n(true);

        // start hubs and fetch initial data
        const fetchPromise = fetch().then(() => console.info('[AppStore] Initial data fetched'));
        const startHubsPromise = startHubs().then(() => console.info('[AppStore] Hubs started'));
        Promise.all([fetchPromise, startHubsPromise])
            .catch((e) => {
                const auth = useAuth();
                auth.logout();
                console.error('[AppStore] Something went wrong. Logout now!');
                console.error(e);
            })
            .finally(() => {
                isLoading.value = false;
            });
    }

    async function startHubs() {
        return Promise.all([ChatHub.start(), CharacterHub.start()]);
    }

    async function stopHubs() {
        return Promise.all([ChatHub.stop(), CharacterHub.stop()]);
    }

    return { initialize, isLoading };
});
