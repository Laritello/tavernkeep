<script setup lang="ts">
import { useRouter } from 'vue-router';

import { useAuth } from '@/composables/useAuth';
import { useSession } from '@/composables/useSession';
import { useModal } from '@/composables/useModal';
import ConfirmationDialog from '@/components/dialogs/ConfirmationDialog.vue';
import HealthBar from '@/components/character/HealthBar.vue';

import { UserRole } from '@/contracts/enums';
import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';
import ArmorClassWidget from '@/components/character/ArmorClassWidget.vue';
import PerceptionWidget from '@/components/character/PerceptionWidget.vue';

const session = useSession();
const router = useRouter();
const user = useCurrentUserAccount();

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
    <div class="navbar bg-base-100 z-10 min-h-fit">
        <div class="relative navbar-start">
            <div v-if="user.activeCharacter.value !== undefined" class="block absolute left-8 bottom-0">
                <HealthBar @click="() => console.log('Health edit')" :health="user.activeCharacter.value.health"
                    class="w-40 h-6" />
            </div>
            <div class="avatar">
                <div class="w-16 rounded-full">
                    <img alt="Tailwind CSS Navbar component"
                        src="https://img.daisyui.com/images/stock/photo-1534528741775-53994a69daeb.webp" />
                </div>
            </div>
            <div v-if="user.activeCharacter.value !== undefined" class="flex flex-col self-stretch justify-start">
                <div class="text-xl font-bold">{{ user.activeCharacter.value.name }}</div>
            </div>
        </div>

        <div v-if="user.activeCharacter.value !== undefined" class="navbar-end">
            <PerceptionWidget :perception="user.activeCharacter.value.perception" class="w-12" />
            <ArmorClassWidget :armor="user.activeCharacter.value.armor" class="w-12" />
        </div>
    </div>
</template>

<style scoped></style>
