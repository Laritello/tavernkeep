<script setup lang="ts">
import { useI18n } from 'vue-i18n';
import { useRouter } from 'vue-router';

import ConfirmationDialog from '@/components/dialogs/ConfirmationDialog.vue';
import LanguageSwitcher from '@/components/shared/LanguageSwitcher.vue';
import ThemeSwitcher from '@/components/shared/ThemeSwitcher.vue';
import { useAuth } from '@/composables/useAuth';
import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';
import { useModal } from '@/composables/useModal';

const router = useRouter();
const user = useCurrentUserAccount();

const { t } = useI18n();

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
            <button class="btn btn-outline btn-xs mx-4 uppercase" @click="router.push('/characters/build')">
                {{ $t('settings.characters.create') }}
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
                        <div class="text-sm font-semithin">
                            {{ character.ancestry.name }} {{ character.class.name }}
                        </div>
                    </div>

                    <div class="dropdown dropdown-left self-center">
                        <button tabindex="0" class="btn btn-circle btn-ghost">
                            <svg
                                xmlns="http://www.w3.org/2000/svg"
                                class="h-6 w-6"
                                viewBox="0 -960 960 960"
                                fill="currentColor"
                            >
                                <path
                                    d="M240-400q-33 0-56.5-23.5T160-480q0-33 23.5-56.5T240-560q33 0 56.5 23.5T320-480q0 33-23.5 56.5T240-400Zm240 0q-33 0-56.5-23.5T400-480q0-33 23.5-56.5T480-560q33 0 56.5 23.5T560-480q0 33-23.5 56.5T480-400Zm240 0q-33 0-56.5-23.5T640-480q0-33 23.5-56.5T720-560q33 0 56.5 23.5T800-480q0 33-23.5 56.5T720-400Z"
                                />
                            </svg>
                        </button>
                        <ul
                            tabindex="0"
                            class="dropdown-content menu bg-base-100 rounded-box z-[1] w-52 p-2 shadow border border-base-200"
                        >
                            <li>
                                <a>{{ t('settings.characters.setActive') }}</a>
                            </li>
                            <li>
                                <a class="text-red-600">{{ t('settings.characters.delete') }}</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

        <p class="font-semibold px-4 text-slate-500 uppercase">{{ t('settings.themes.header') }}</p>

        <ThemeSwitcher />

        <p class="font-semibold px-4 text-slate-500 uppercase">{{ t('settings.language.header') }}</p>
        <LanguageSwitcher class="mt-2" />

        <div class="divider px-4"></div>

        <button @click.prevent="logout" class="btn btn-ghost justify-start text-red-600">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" viewBox="0 -960 960 960" fill="currentColor">
                <path
                    d="M200-120q-33 0-56.5-23.5T120-200v-560q0-33 23.5-56.5T200-840h280v80H200v560h280v80H200Zm440-160-55-58 102-102H360v-80h327L585-622l55-58 200 200-200 200Z"
                />
            </svg>
            {{ t('settings.logout') }}
        </button>
    </div>
</template>
