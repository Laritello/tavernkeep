import './assets/main.css';
import '@mdi/font/css/materialdesignicons.css';

import { createApp } from 'vue';
import App from './App.vue';

import { createPinia } from 'pinia';
// @ts-ignore
import VueChatScroll from 'vue3-chat-scroll';

// Router
import { router } from './router';
import { useAppState } from './stores/appState';

const app = createApp(App);
const pinia = createPinia();
app.use(pinia);
app.use(router);
app.use(VueChatScroll);

useAppState();

app.mount('#app');
