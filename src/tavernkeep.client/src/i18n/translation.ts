import i18n from '@/i18n/index';

export default {
    get supportedLocales(): string[] {
        console.log(import.meta.env);
        return import.meta.env.VITE_SUPPORTED_LOCALES.split(",")
    },

    set currentLocale(newLocale: string) {
        i18n.global.locale.value = newLocale
    },

    get currentLocale() {
        return i18n.global.locale.value;
    },
    
    get defaultLocale() {
        return import.meta.env.VITE_DEFAULT_LOCALE
    },
    
    isLocaleSupported(locale: string) {
        return this.supportedLocales.includes(locale)
    },

    getUserLocale() {
        const locale = window.navigator.language ||
            window.navigator.language ||
            this.defaultLocale
        return {
            locale: locale,
            localeNoRegion: locale.split('-')[0]
        }
    },

    getPersistedLocale() {
        const persistedLocale = localStorage.getItem("user-locale");
        if(persistedLocale === null) {
            return null;
        }
        
        if(this.isLocaleSupported(persistedLocale)) {
            return persistedLocale
        }

        return null;
    },
    
    async switchLanguage(newLocale: string) {
        if(document === null) return;
        this.currentLocale = newLocale;
        document.querySelector("html")?.setAttribute("lang", newLocale);
        localStorage.setItem("user-locale", newLocale);
    }
} as const;