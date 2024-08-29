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
    <!-- Mobile layout -->
    <div class="flex flex-col overflow-clip h-full">
        <!-- Navbar -->
        <header class="navbar bg-base-300 w-full">
            <div class="flex-none gap-2">
                <div class="dropdown">
                    <div tabindex="0" role="button" class="btn btn-ghost btn-circle avatar">
                        <div class="w-10 rounded-full">
                            <img
                                alt="Tailwind CSS Navbar component"
                                src="https://img.daisyui.com/images/stock/photo-1534528741775-53994a69daeb.webp"
                            />
                        </div>
                    </div>
                    <ul
                        tabindex="0"
                        class="menu menu-sm dropdown-content bg-base-300 rounded-box z-[1] mt-3 w-52 p-2 shadow-lg"
                    >
                        <li>
                            <a class="justify-between" @click="$router.push('/characters')">
                                {{ session.userLogin.value }}
                                <span class="badge font-bold">Characters</span>
                            </a>
                        </li>
                        <li v-if="session.userRole.value === UserRole.Master">
                            <a @click="$router.push('/admin')">GM panel</a>
                        </li>
                        <li>
                            <label class="swap swap-rotate">
                                Toggle theme
                                <!-- this hidden checkbox controls the state -->
                                <input type="checkbox" class="theme-controller" value="dark" />

                                <!-- sun icon -->
                                <svg
                                    class="swap-off h-10 w-10 fill-current"
                                    xmlns="http://www.w3.org/2000/svg"
                                    viewBox="0 0 24 24"
                                >
                                    <path
                                        d="M5.64,17l-.71.71a1,1,0,0,0,0,1.41,1,1,0,0,0,1.41,0l.71-.71A1,1,0,0,0,5.64,17ZM5,12a1,1,0,0,0-1-1H3a1,1,0,0,0,0,2H4A1,1,0,0,0,5,12Zm7-7a1,1,0,0,0,1-1V3a1,1,0,0,0-2,0V4A1,1,0,0,0,12,5ZM5.64,7.05a1,1,0,0,0,.7.29,1,1,0,0,0,.71-.29,1,1,0,0,0,0-1.41l-.71-.71A1,1,0,0,0,4.93,6.34Zm12,.29a1,1,0,0,0,.7-.29l.71-.71a1,1,0,1,0-1.41-1.41L17,5.64a1,1,0,0,0,0,1.41A1,1,0,0,0,17.66,7.34ZM21,11H20a1,1,0,0,0,0,2h1a1,1,0,0,0,0-2Zm-9,8a1,1,0,0,0-1,1v1a1,1,0,0,0,2,0V20A1,1,0,0,0,12,19ZM18.36,17A1,1,0,0,0,17,18.36l.71.71a1,1,0,0,0,1.41,0,1,1,0,0,0,0-1.41ZM12,6.5A5.5,5.5,0,1,0,17.5,12,5.51,5.51,0,0,0,12,6.5Zm0,9A3.5,3.5,0,1,1,15.5,12,3.5,3.5,0,0,1,12,15.5Z"
                                    />
                                </svg>

                                <!-- moon icon -->
                                <svg
                                    class="swap-on h-10 w-10 fill-current"
                                    xmlns="http://www.w3.org/2000/svg"
                                    viewBox="0 0 24 24"
                                >
                                    <path
                                        d="M21.64,13a1,1,0,0,0-1.05-.14,8.05,8.05,0,0,1-3.37.73A8.15,8.15,0,0,1,9.08,5.49a8.59,8.59,0,0,1,.25-2A1,1,0,0,0,8,2.36,10.14,10.14,0,1,0,22,14.05,1,1,0,0,0,21.64,13Zm-9.5,6.69A8.14,8.14,0,0,1,7.08,5.22v.27A10.15,10.15,0,0,0,17.22,15.63a9.79,9.79,0,0,0,2.1-.22A8.11,8.11,0,0,1,12.14,19.73Z"
                                    />
                                </svg>
                            </label>
                        </li>
                        <li class="pt-2"><a @click.prevent="logout">Logout</a></li>
                    </ul>
                </div>
                <div v-if="user.activeCharacter.value !== undefined" class="block">
                    <div class="text-xl font-bold">{{ user.activeCharacter.value.name }}</div>
                    <HealthBar
                        @click="() => console.log('Health edit')"
                        :health="user.activeCharacter.value.health"
                        class="w-32"
                    />
                </div>
            </div>

            <div v-if="user.activeCharacter.value !== undefined" class="flex w-full justify-end">
                <PerceptionWidget :perception="user.activeCharacter.value.perception" class="w-12 h-12" />
                <ArmorClassWidget :armor="user.activeCharacter.value.armor" class="w-12 h-12" />
            </div>
        </header>
        <!-- /Navbar -->
        <!-- Page content here -->
        <div class="content-height">
            <slot />
        </div>
        <!--  Bottom navbar  -->
        <footer class="sticky bottom-0 btm-nav min-h-16 lg:hidden">
            <RouterLink active-class="active text-info" to="/combat" class="fill-neutral-500">
                <svg width="32px" height="32px" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg">
                    <path d="M3 0L6.58579 3.58579L3.58579 6.58579L0 3V0H3Z" />
                    <path
                        d="M6.70711 12.2929L8.20711 13.7929L6.79289 15.2071L4.5 12.9142L2.99771 14.4165C2.99923 14.4441 3 14.472 3 14.5C3 15.3284 2.32843 16 1.5 16C0.671573 16 0 15.3284 0 14.5C0 13.6716 0.671573 13 1.5 13C1.52802 13 1.55586 13.0008 1.5835 13.0023L3.08579 11.5L0.792893 9.20711L2.20711 7.79289L3.70711 9.29289L13 0H16V3L6.70711 12.2929Z"
                    />
                    <path
                        d="M14.5 16C13.6716 16 13 15.3284 13 14.5C13 14.472 13.0008 14.4441 13.0023 14.4165L10.0858 11.5L13.7929 7.79289L15.2071 9.20711L12.9142 11.5L14.4165 13.0023C14.4441 13.0008 14.472 13 14.5 13C15.3284 13 16 13.6716 16 14.5C16 15.3284 15.3284 16 14.5 16Z"
                    />
                </svg>
            </RouterLink>
            <RouterLink active-class="active text-info" to="/" class="fill-neutral-500">
                <svg height="32px" width="32px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                    <g>
                        <path
                            d="M458.159,404.216c-18.93-33.65-49.934-71.764-100.409-93.431c-28.868,20.196-63.938,32.087-101.745,32.087
		c-37.828,0-72.898-11.89-101.767-32.087c-50.474,21.667-81.479,59.782-100.398,93.431C28.731,448.848,48.417,512,91.842,512
		c43.426,0,164.164,0,164.164,0s120.726,0,164.153,0C463.583,512,483.269,448.848,458.159,404.216z"
                        />
                        <path
                            d="M256.005,300.641c74.144,0,134.231-60.108,134.231-134.242v-32.158C390.236,60.108,330.149,0,256.005,0
		c-74.155,0-134.252,60.108-134.252,134.242V166.4C121.753,240.533,181.851,300.641,256.005,300.641z"
                        />
                    </g>
                </svg>
            </RouterLink>
            <RouterLink active-class="active text-info" to="/chat">
                <svg
                    width="48px"
                    height="48px"
                    viewBox="0 0 24 24"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                    class="fill-neutral-500"
                >
                    <path
                        d="M13.6288 20.4718L13.0867 21.3877C12.6035 22.204 11.3965 22.204 10.9133 21.3877L10.3712 20.4718C9.95073 19.7614 9.74049 19.4063 9.40279 19.2098C9.06509 19.0134 8.63992 19.0061 7.78958 18.9915C6.53422 18.9698 5.74689 18.8929 5.08658 18.6194C3.86144 18.1119 2.88807 17.1386 2.3806 15.9134C2 14.9946 2 13.8297 2 11.5V10.5C2 7.22657 2 5.58985 2.7368 4.38751C3.14908 3.71473 3.71473 3.14908 4.38751 2.7368C5.58985 2 7.22657 2 10.5 2H13.5C16.7734 2 18.4101 2 19.6125 2.7368C20.2853 3.14908 20.8509 3.71473 21.2632 4.38751C22 5.58985 22 7.22657 22 10.5V11.5C22 13.8297 22 14.9946 21.6194 15.9134C21.1119 17.1386 20.1386 18.1119 18.9134 18.6194C18.2531 18.8929 17.4658 18.9698 16.2104 18.9915C15.36 19.0061 14.9349 19.0134 14.5972 19.2098C14.2595 19.4062 14.0492 19.7614 13.6288 20.4718Z"
                    />
                    <path
                        d="M17 11C17 11.5523 16.5523 12 16 12C15.4477 12 15 11.5523 15 11C15 10.4477 15.4477 10 16 10C16.5523 10 17 10.4477 17 11Z"
                        class="fill-neutral-700"
                    />
                    <path
                        d="M13 11C13 11.5523 12.5523 12 12 12C11.4477 12 11 11.5523 11 11C11 10.4477 11.4477 10 12 10C12.5523 10 13 10.4477 13 11Z"
                        class="fill-neutral-700"
                    />
                    <path
                        d="M9 11C9 11.5523 8.55228 12 8 12C7.44772 12 7 11.5523 7 11C7 10.4477 7.44772 10 8 10C8.55228 10 9 10.4477 9 11Z"
                        class="fill-neutral-700"
                    />
                </svg>
            </RouterLink>
        </footer>
        <!--  /Bottom navbar  -->
    </div>
    <!-- /Mobile layout -->
</template>

<style scoped>
.content-height {
    height: calc(100% - 128px);
}
</style>
