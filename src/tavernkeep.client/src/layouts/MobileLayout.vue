<script setup lang="ts">
import { useAuth } from '@/composables/useAuth';
import { useSession } from '@/composables/useSession';
import { useRouter } from 'vue-router';

const session = useSession();
const router = useRouter();

async function logout() {
    const auth = useAuth();
    auth.logout();
    await router.push('/login');
}
</script>

<template>
    <div class="flex flex-col h-screen">
        <!-- Navbar -->
        <div class="relative navbar bg-primary text-primary-content z-[1] shadow-sm shadow-neutral-950">
            <div class="flex-1">
                <h1 class="text-xl px-4 cursor-default">Tavernkeep</h1>
            </div>
            <label class="flex cursor-pointer gap-2 mr-4">
                <span class="label-text text-primary-content">Light</span>
                <input type="checkbox" value="dark" class="toggle theme-controller" />
                <span class="label-text text-primary-content">Dark</span>
            </label>
            <div class="flex-none text-base-content">
                <span class="text-primary-content">{{ session.userLogin }}</span>
                <div class="dropdown dropdown-end">
                    <div tabindex="0" role="button" class="btn btn-ghost btn-circle avatar">
                        <div class="avatar w-10 rounded-full bg-orange-400"></div>
                    </div>
                    <ul
                        tabindex="0"
                        class="menu menu-sm dropdown-content mt-3 z-[1] p-2 shadow bg-base-100 rounded-md w-52"
                    >
                        <li>
                            <a @click.prevent="logout">Logout</a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="flex flex-row content-height">
            <slot />
        </div>
    </div>
</template>

<style scoped>
.content-height {
    height: calc(100% - 4rem);
}

.router-link-active {
    @apply active;
}
</style>
