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
                    secondary: '#efbf04',
                },
                dark: {
                    ...require('daisyui/src/theming/themes')['dark'],
                    primary: '#29B6F6',
                    secondary: '#efbf04',
                },
            },
        ],
    },
    darkMode: ['selector', '[data-theme="dark"]'],
};
