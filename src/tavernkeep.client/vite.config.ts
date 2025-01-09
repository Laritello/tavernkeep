import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import VueI18nPlugin from '@intlify/unplugin-vue-i18n/vite'
import svgLoader from 'vite-svg-loader';
import fs from 'fs';
import path from 'path';
import { resolve, dirname } from 'node:path'
import child_process from 'child_process';

const baseFolder =
    process.env.APPDATA !== undefined && process.env.APPDATA !== ''
        ? `${process.env.APPDATA}/ASP.NET/https`
        : `${process.env.HOME}/.aspnet/https`;

// const certificateArg = process.argv
//     .map((arg) => arg.match(/--name=(?<value>.+)/i))
//     .filter(Boolean)[0];
// const certificateName = certificateArg
//     ? certificateArg.groups.value
//     : 'tavernkeep.client';

// if (!certificateName) {
//     console.error(
//         'Invalid certificate name. Run this script in the context of an npm/yarn script or pass --name=<<app>> explicitly.'
//     );
//     process.exit(-1);
// }

const certificateName = 'tavernkeep.client';
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (
        0 !==
        child_process.spawnSync(
            'dotnet',
            ['dev-certs', 'https', '--export-path', certFilePath, '--format', 'Pem', '--no-password'],
            { stdio: 'inherit' }
        ).status
    ) {
        throw new Error('Could not create certificate.');
    }
}
// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
        vue(),
        svgLoader(),
        VueI18nPlugin({
            include: resolve(dirname(fileURLToPath(import.meta.url)), './src/i18n/locales/**'), // provide a path to the folder where you'll store translation data (see below)
        })
    ],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url)),
        },
    },
    server: {
        host: '0.0.0.0',
        port: 5173,
    },
});
