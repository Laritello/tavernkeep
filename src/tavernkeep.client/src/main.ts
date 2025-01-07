import './assets/main.css';
import '@mdi/font/css/materialdesignicons.css';
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";
import D20Icon from '@/assets/dice/d20-grey.svg';

import { createApp } from 'vue';
import i18n from "./i18n"
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
app.use(i18n);
app.use(VueChatScroll);
app.use(Toast, {
    transition: "Vue-Toastification__bounce",
    maxToasts: 3,
    newestOnTop: true,
    toastDefaults: {
        default: {
            timeout: 5000,
            showCloseButtonOnHover: false,
            closeButton: false,
            icon: D20Icon,
        }
    }
});

useAppState();

app.mount('#app');
