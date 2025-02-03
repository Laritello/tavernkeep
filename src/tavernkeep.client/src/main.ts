import ContextMenu from '@imengyu/vue3-context-menu';
import '@imengyu/vue3-context-menu/lib/vue3-context-menu.css';
import '@mdi/font/css/materialdesignicons.css';
import { createPinia } from 'pinia';
import { createApp } from 'vue';
import Toast from 'vue-toastification';
import 'vue-toastification/dist/index.css';

import D20Icon from '@/assets/dice/d20-grey.svg';

import App from './App.vue';
import './assets/main.css';
import i18n from './i18n';
// Router
import { router } from './router';
import { useAppState } from './stores/appState';

const app = createApp(App);
const pinia = createPinia();
app.use(pinia);
app.use(router);
app.use(i18n);
app.use(ContextMenu);
app.use(Toast, {
    transition: 'Vue-Toastification__bounce',
    maxToasts: 3,
    newestOnTop: true,
    toastDefaults: {
        default: {
            timeout: 5000,
            showCloseButtonOnHover: false,
            closeButton: false,
            icon: D20Icon,
        },
    },
});

useAppState();

app.mount('#app');
