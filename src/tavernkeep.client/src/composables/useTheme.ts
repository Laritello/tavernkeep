import { useDark, useToggle } from '@vueuse/core';
import { computed } from 'vue';

const DARK_NAME = 'dark';
const LIGHT_NAME = 'light';

export const useTheme = () => {
    const isDark = useDark({
        selector: document.documentElement,
        attribute: 'data-theme',
        storageKey: 'tavernkeep.theme',
        valueDark: 'dark',
        valueLight: 'light',
    });

    const toggleDark = useToggle(isDark);
    const theme = computed(() => (isDark.value ? DARK_NAME : LIGHT_NAME));

    return { theme, isDark, toggleDark };
};
