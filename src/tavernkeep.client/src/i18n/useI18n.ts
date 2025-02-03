import { useLocalStorage } from '@vueuse/core';
import { watch } from 'vue';

import { i18n, type LocaleType } from '@/i18n';

export const useI18n = (initialize = false) => {
    const userPreferredLanguage = window.navigator.language.split('-')[0];
    const defaultLanguage: LocaleType = i18n.global.availableLocales.includes(userPreferredLanguage)
        ? userPreferredLanguage
        : import.meta.env.VITE_DEFAULT_LOCALE;
    const locale = useLocalStorage<LocaleType>('tavernkeep.locale', defaultLanguage);

    if (initialize) {
        i18n.global.locale.value = locale.value;
    }

    watch(locale, (newValue) => {
        i18n.global.locale.value = newValue;
    });
    const changeLocale = (value: LocaleType) => (locale.value = value);

    return { ...i18n.global, locale, changeLocale };
};
