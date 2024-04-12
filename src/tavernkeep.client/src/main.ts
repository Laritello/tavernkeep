import './assets/main.css';
import '@mdi/font/css/materialdesignicons.css';

import { createApp, watch } from 'vue';
import App from './App.vue';

import { createPinia } from 'pinia';
// @ts-ignore
import VueChatScroll from 'vue3-chat-scroll';

// Vuetify
import 'vuetify/styles';
import { createVuetify } from 'vuetify';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';

// Router
import { router } from './router';
import ChatHub from './api/hubs/ChatHub';
import CharacterHub from './api/hubs/CharacterHub';

import { useSession } from './composables/useSession';
import { useUsers } from './stores/users';
import { useCharacters } from './stores/characters';
import { useMessages } from './stores/messages';

const vuetify = createVuetify({
    components,
    directives,
    theme: {
        defaultTheme: 'light',
    },
});

const app = createApp(App);
const pinia = createPinia();
app.use(vuetify);
app.use(pinia);
app.use(router);
app.use(VueChatScroll);

const session = useSession();
const users = useUsers();
const characters = useCharacters();
const messages = useMessages();

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

app.mount('#app');
