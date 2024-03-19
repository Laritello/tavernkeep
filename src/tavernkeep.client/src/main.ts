import 'reflect-metadata';
import './assets/main.css';
import '@mdi/font/css/materialdesignicons.css';

import { createApp } from 'vue';
import App from './App.vue';
import { useAppStore } from './stores/app.store';

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

useAppStore(); // Initialize store

app.mount('#app');
