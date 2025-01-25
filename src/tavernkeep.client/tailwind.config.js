/* eslint-disable @typescript-eslint/no-require-imports */
/** @type {import('tailwindcss').Config} */
export default {
    content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
    theme: {
        extend: {},
    },
    plugins: [require('daisyui')],
    daisyui: {
        //themes: ['light', 'dark'],
        themes: [
            {
                light: {
                    ...require('daisyui/src/theming/themes')['light'],
                    primary: '#29B6F6',
                    secondary: '#66BB6A',
                },
                dark: {
                    ...require('daisyui/src/theming/themes')['dark'],
                    primary: '#29B6F6',
                    secondary: '#66BB6A',
                },
            },
        ],
    },
    darkMode: ['selector', '[data-theme="dark"]'],
};
