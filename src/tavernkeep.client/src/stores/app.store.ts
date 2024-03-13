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
        fetch().then(() => {
            console.info('[AppStore] Initial data fetched');
        });
        const chatHubPromise = ChatHub.start().then(() => console.info('[AppStore] ChatHub started'));
        const charHubPromise = CharacterHub.start().then(() => console.info('[AppStore] CharacterHub started'));
        Promise.all([chatHubPromise, charHubPromise]).then(() => {
            console.groupEnd();
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
        fetch();
        ChatHub.start().then(() => console.info('[AppStore] ChatHub started'));
        CharacterHub.start().then(() => console.info('[AppStore] CharacterHub started'));
    });

    async function fetch(): Promise<void[]> {
        return Promise.all([users.fetch(), characters.fetch(), messages.fetch(0, 20)]);
    }

    return { auth, users, characters, messages };
});
