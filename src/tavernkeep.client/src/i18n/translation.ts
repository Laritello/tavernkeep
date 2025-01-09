import i18n from '@/i18n/index';

export default {
    get supportedLocales(): string[] {
        return import.meta.env.VITE_SUPPORTED_LOCALES.split(",")
    },

    set currentLocale(newLocale: string) {
        i18n.global.locale.value = newLocale
    },

    get currentLocale() {
        return i18n.global.locale.value;
    },
    
    get defaultLocale(): string {
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

    guessDefaultLocale(): string {
        // try to load from local storage
        const userPersistedLocale = this.getPersistedLocale();
        if(userPersistedLocale) {
            return userPersistedLocale;
        }
        
        // try to load from browser settings
        const userPreferredLocale = this.getUserLocale();
        if (this.isLocaleSupported(userPreferredLocale.locale)) {
            return userPreferredLocale.locale;
        }
        
        // check is locale supported
        if (this.isLocaleSupported(userPreferredLocale.localeNoRegion)) {
            return userPreferredLocale.localeNoRegion;
        }
        
        // fallback to default locale
        return this.defaultLocale;
    },
    
    switchLanguage(newLocale: string) {
        if(document === null) return;
        this.currentLocale = newLocale;
        document.querySelector("html")?.setAttribute("lang", newLocale);
        localStorage.setItem("user-locale", newLocale);
    }
} as const;