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
</script>

<template>
    <div class="navbar bg-base-100 z-10 min-h-fit border-b-2">
        <div class="relative navbar-start">
            <div v-if="user.activeCharacter.value !== undefined" class="block absolute left-8 bottom-0">
                <HealthBar @click="() => console.log('Health edit')" :health="user.activeCharacter.value.health"
                    class="min-w-40 h-6" />
            </div>
            <div class="avatar">
                <div class="w-16 rounded-full">
                    <img alt="Tailwind CSS Navbar component"
                        src="https://www.fantasyflightgames.com/media/ffg_content/dark-heresy/images/WH_Pushed-to-the-Limit_HRF_090918_IFS.jpg" />
                </div>
            </div>
            <div v-if="user.activeCharacter.value !== undefined" class="flex flex-col self-stretch justify-start pt-1">
                <p class="text-md font-bold  leading-tight antialiased text-clip text-nowrap">
                    {{ user.activeCharacter.value.name }}
                </p>
                <p class="text-xs font-normal leading-tight">
                    {{ user.activeCharacter.value.ancestry }} {{ user.activeCharacter.value.class }} {{
                        user.activeCharacter.value.level }}
                </p>
            </div>
        </div>

        <div v-if="user.activeCharacter.value !== undefined" class="navbar-end">
            <PerceptionWidget :perception="user.activeCharacter.value.perception" class="w-12" />
            <ArmorClassWidget :armor="user.activeCharacter.value.armor" class="w-12" />
        </div>
    </div>
</template>

<style scoped></style>
