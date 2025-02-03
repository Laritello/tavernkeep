﻿import { createI18n } from 'vue-i18n';

import en from './locales/en.json';
import ru from './locales/ru.json';

export type LocaleType = 'en' | 'ru';

export const i18n = createI18n({
    locale: import.meta.env.VITE_DEFAULT_LOCALE as LocaleType,
    fallbackLocale: import.meta.env.VITE_FALLBACK_LOCALE as LocaleType,
    legacy: false,
    globalInjection: true,
    messages: {
        en,
        ru,
    },
});
