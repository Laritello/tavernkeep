import { useLocalStorage } from '@vueuse/core';
import { watch } from 'vue';

import i18n from '@/i18n';

export type LocaleType = typeof i18n.global.locale.value;

export const useI18n = (initialize = false) => {
    const locale = useLocalStorage<LocaleType>(
        'tavernkeep.locale',
        window.navigator.language || import.meta.env.VITE_DEFAULT_LOCALE
    );

    if (initialize) {
        i18n.global.locale.value = locale.value;
    }

    watch(locale, (newValue) => {
        i18n.global.locale.value = newValue;
    });
    const changeLocale = (value: LocaleType) => (locale.value = value);

    return { ...i18n.global, locale, changeLocale };
};
