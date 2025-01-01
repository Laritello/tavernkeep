<script setup lang="ts">
import { useRouter } from 'vue-router';

import { useAuth } from '@/composables/useAuth';
import { useSession } from '@/composables/useSession';
import { useModal } from '@/composables/useModal';
import ConfirmationDialog from '@/components/dialogs/ConfirmationDialog.vue';

import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';

const router = useRouter();

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
    <div class="flex flex-col">
        <p class="font-semibold px-4 text-slate-500">THEMES</p>
        <div class="flex flex-col px-4">
            <div class="form-control">
                <label class="label cursor-pointer">
                    <span class="label-text">Red pill</span>
                    <input type="radio" name="radio-10" class="radio theme-controller" value="default" />
                </label>
            </div>
            <div class="form-control">
                <label class="label cursor-pointer">
                    <span class="label-text">Blue pill</span>
                    <input type="radio" name="radio-10" class="radio theme-controller" value="dark" />
                </label>
            </div>
        </div>

        <div class="divider px-4"></div>

        <button @click.prevent="logout" class="btn btn-ghost justify-start text-red-600">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" viewBox="0 -960 960 960" fill="currentColor">
                <path
                    d="M200-120q-33 0-56.5-23.5T120-200v-560q0-33 23.5-56.5T200-840h280v80H200v560h280v80H200Zm440-160-55-58 102-102H360v-80h327L585-622l55-58 200 200-200 200Z" />
            </svg>
            Logout
        </button>
    </div>
</template>

<style scoped></style>
