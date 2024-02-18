import './assets/main.css';
import '@mdi/font/css/materialdesignicons.css';

import { createApp } from 'vue';
import App from './App.vue';

import { createPinia } from 'pinia';

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
app.mount('#app');
