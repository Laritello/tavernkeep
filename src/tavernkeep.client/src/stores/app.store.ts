import { watch } from 'vue';
import { defineStore, storeToRefs } from 'pinia';

import { useAuthStore } from './auth.store';
import { useUsersStore } from './users.store';
import { useCharactersStore } from './characters.store';
import { useMessagesStore } from './messages.store';

export const useAppStore = defineStore('app.store', () => {
    const auth = useAuthStore();
    const users = useUsersStore();
    const characters = useCharactersStore();
    const messages = useMessagesStore();

    const { isLoggedIn } = storeToRefs(auth);

    console.info('[AppStore] Initialize');
    if (isLoggedIn.value) {
        console.info('[AppStore] User already logged in. Fetching data...');
        fetch();
    }

    watch(isLoggedIn, (value): void => {
        console.info('[AppStore] Auth status update');
        if (!value) {
            console.info('[AppStore] User logged out');
            return;
        }
        console.info('[AppStore] User logged in. Fetching data...');
        fetch();
    });

    function fetch(): void {
        users.fetch();
        characters.fetch();
        messages.fetch(0, 20);
    }

    return { auth, users, characters, messages };
});
