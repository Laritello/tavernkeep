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

    console.info('[AppStore] Initialize');
    if (isLoggedIn.value) {
        console.info('[AppStore] User already logged in. Fetching data...');
        // TODO: Fix me
        // HACK: Fetch data when user is logged in and connect to hubs AFTER it
        fetch().then(() => {
            console.info('[AppStore] Initial data fetched');
            ChatHub.start().then(() => console.info('[AppStore] ChatHub started'));
            CharacterHub.start().then(() => console.info('[AppStore] CharacterHub started'));
        });
    }

    watch(isLoggedIn, (value): void => {
        console.info('[AppStore] Auth status update');
        if (!value) {
            console.info('[AppStore] User logged out');
            ChatHub.stop().then(() => console.info('[AppStore] ChatHub stopped'));
            CharacterHub.stop().then(() => console.info('[AppStore] CharacterHub stopped'));
            return;
        }
        console.info('[AppStore] User logged in. Fetching data...');
        fetch().then(async () => {
            ChatHub.start().then(() => console.info('[AppStore] ChatHub started'));
            CharacterHub.start().then(() => console.info('[AppStore] CharacterHub started'));
        });
    });

    async function fetch(): Promise<void> {
        await Promise.race([users.fetch(), characters.fetch(), messages.fetch(0, 20)]);
    }

    return { auth, users, characters, messages };
});
