<script setup lang="ts">
import { useRouter } from 'vue-router';
import { useAuth } from '@/composables/useAuth';
import { useModal } from '@/composables/useModal';
import ConfirmationDialog from '@/components/dialogs/ConfirmationDialog.vue';
import LanguageSwitcher from '@/components/shared/LanguageSwitcher.vue';
import { useI18n } from 'vue-i18n';
import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';

const router = useRouter();
const user = useCurrentUserAccount();

const { t } = useI18n();

// Change the theme dynamically
const changeTheme = (name: string): void => {
    document.documentElement.setAttribute("data-theme", name);
    localStorage.setItem("theme", name);
};

function setLight() {
    changeTheme('light')
}


function setDark() {
    changeTheme('dark')
}

async function logout() {
    const modal = useModal();
    const modalResult = await modal.show(ConfirmationDialog, {
        caption: 'Logout',
        message: 'You sure you want to logout?',
    });

    if (modalResult.action !== 'confirm') return;
    const auth = useAuth();
    auth.logout();
    await router.push('/login');
}
</script>

<template>
    <div class="flex flex-col h-full pt-2">
        <div class="flex flex-row justify-between items-center">
            <h1 class="font-semibold px-4 text-slate-500 uppercase">{{ t('settings.characters.header') }}</h1>
            <button class="btn btn-outline btn-xs mx-4" @click="router.push('/characters/create')">
                Create new
            </button>
        </div>

        <div class="flex flex-col px-4">
            <div v-for="character in user.characters.value" :key="character.id" class="my-2">
                <div class="flex flex-row gap-2 border border-slate-500 rounded-xl p-2">
                    <div class="avatar">
                        <div class="w-14 h-14 rounded-xl">
                            <img src="https://img.daisyui.com/images/stock/photo-1534528741775-53994a69daeb.webp" />
                        </div>
                    </div>

                    <div class="flex flex-col flex-1 align-middle justify-center">
                        <div class="text-lg font-semibold leading-none">{{ character.name }}</div>
                        <div class="text-sm font-semithin">{{ character.ancestry }} {{ character.class }}</div>
                    </div>

                    <div class="dropdown dropdown-left self-center">
                        <button tabindex="0" class="btn btn-circle btn-ghost">
                            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" viewBox="0 -960 960 960"
                                fill="currentColor">
                                <path
                                    d="M240-400q-33 0-56.5-23.5T160-480q0-33 23.5-56.5T240-560q33 0 56.5 23.5T320-480q0 33-23.5 56.5T240-400Zm240 0q-33 0-56.5-23.5T400-480q0-33 23.5-56.5T480-560q33 0 56.5 23.5T560-480q0 33-23.5 56.5T480-400Zm240 0q-33 0-56.5-23.5T640-480q0-33 23.5-56.5T720-560q33 0 56.5 23.5T800-480q0 33-23.5 56.5T720-400Z" />
                            </svg>
                        </button>
                        <ul tabindex="0" class="dropdown-content menu bg-base-100 rounded-box z-[1] w-52 p-2 shadow border border-base-200">
                            <li><a>Select character as active</a></li>
                            <li><a>Delete character</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

        <p class="font-semibold px-4 text-slate-500 uppercase">{{ t('settings.themes.header') }}</p>

        <button v-on:click="setLight" class="btn btn-ghost justify-start">
            <svg class="h-6 w-6" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor">
                <path
                    d="M5.64,17l-.71.71a1,1,0,0,0,0,1.41,1,1,0,0,0,1.41,0l.71-.71A1,1,0,0,0,5.64,17ZM5,12a1,1,0,0,0-1-1H3a1,1,0,0,0,0,2H4A1,1,0,0,0,5,12Zm7-7a1,1,0,0,0,1-1V3a1,1,0,0,0-2,0V4A1,1,0,0,0,12,5ZM5.64,7.05a1,1,0,0,0,.7.29,1,1,0,0,0,.71-.29,1,1,0,0,0,0-1.41l-.71-.71A1,1,0,0,0,4.93,6.34Zm12,.29a1,1,0,0,0,.7-.29l.71-.71a1,1,0,1,0-1.41-1.41L17,5.64a1,1,0,0,0,0,1.41A1,1,0,0,0,17.66,7.34ZM21,11H20a1,1,0,0,0,0,2h1a1,1,0,0,0,0-2Zm-9,8a1,1,0,0,0-1,1v1a1,1,0,0,0,2,0V20A1,1,0,0,0,12,19ZM18.36,17A1,1,0,0,0,17,18.36l.71.71a1,1,0,0,0,1.41,0,1,1,0,0,0,0-1.41ZM12,6.5A5.5,5.5,0,1,0,17.5,12,5.51,5.51,0,0,0,12,6.5Zm0,9A3.5,3.5,0,1,1,15.5,12,3.5,3.5,0,0,1,12,15.5Z" />
            </svg>
            {{ t('settings.themes.light') }}
        </button>
        <button v-on:click="setDark" class="btn btn-ghost justify-start">
            <svg class="h-6 w-6" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor">
                <path
                    d="M21.64,13a1,1,0,0,0-1.05-.14,8.05,8.05,0,0,1-3.37.73A8.15,8.15,0,0,1,9.08,5.49a8.59,8.59,0,0,1,.25-2A1,1,0,0,0,8,2.36,10.14,10.14,0,1,0,22,14.05,1,1,0,0,0,21.64,13Zm-9.5,6.69A8.14,8.14,0,0,1,7.08,5.22v.27A10.15,10.15,0,0,0,17.22,15.63a9.79,9.79,0,0,0,2.1-.22A8.11,8.11,0,0,1,12.14,19.73Z" />
            </svg>
            {{ t('settings.themes.dark') }}
        </button>

        <p class="font-semibold px-4 text-slate-500 uppercase">{{ t('settings.language.header') }}</p>
        <LanguageSwitcher class="mt-2" />

        <div class="divider px-4"></div>

        <button @click.prevent="logout" class="btn btn-ghost justify-start text-red-600">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" viewBox="0 -960 960 960" fill="currentColor">
                <path
                    d="M200-120q-33 0-56.5-23.5T120-200v-560q0-33 23.5-56.5T200-840h280v80H200v560h280v80H200Zm440-160-55-58 102-102H360v-80h327L585-622l55-58 200 200-200 200Z" />
            </svg>
            {{ t('settings.logout') }}
        </button>
    </div>
</template>

<style scoped></style>
