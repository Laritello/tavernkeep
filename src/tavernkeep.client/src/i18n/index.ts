import { createI18n, type I18nOptions } from 'vue-i18n';

import en from './locales/en.json';
import ru from './locales/ru.json';

export type LocaleType = 'en' | 'ru';

const options: I18nOptions = {
    legacy: false,
    globalInjection: true,

    locale: import.meta.env.VITE_DEFAULT_LOCALE,
    fallbackLocale: import.meta.env.VITE_FALLBACK_LOCALE,

    messages: {
        en,
        ru,
    },
};

export const i18n = createI18n<false, typeof options>(options);
