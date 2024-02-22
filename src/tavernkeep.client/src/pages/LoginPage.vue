<template>
    <div class="flex h-screen w-screen justify-center items-center">
        <v-sheet width="400">
            <form fast-fail @submit.prevent="authorize">
                <div class="grid gap-2">
                    <label class="input input-bordered flex items-center gap-2">
                        <!-- prettier-ignore -->
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="w-4 h-4 opacity-70" ><path d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6ZM12.735 14c.618 0 1.093-.561.872-1.139a6.002 6.002 0 0 0-11.215 0c-.22.578.254 1.139.872 1.139h9.47Z" /></svg>
                        <input v-model="credentials.login" type="text" class="grow" placeholder="Username" required />
                    </label>
                    <label class="input input-bordered flex items-center gap-2">
                        <!-- prettier-ignore -->
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="w-4 h-4 opacity-70" ><path fill-rule="evenodd" d="M14 6a4 4 0 0 1-4.899 3.899l-1.955 1.955a.5.5 0 0 1-.353.146H5v1.5a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1-.5-.5v-2.293a.5.5 0 0 1 .146-.353l3.955-3.955A4 4 0 1 1 14 6Zm-4-2a.75.75 0 0 0 0 1.5.5.5 0 0 1 .5.5.75.75 0 0 0 1.5 0 2 2 0 0 0-2-2Z" clip-rule="evenodd" /></svg>
                        <input
                            v-model="credentials.password"
                            type="password"
                            class="grow"
                            placeholder="Password"
                            required
                        />
                    </label>
                    <button type="submit" class="btn btn-primary grow">
                        <span class="text-primary-content">Sign in</span>
                    </button>
                </div>
            </form>
        </v-sheet>
    </div>
</template>

<script setup lang="ts">
import { useAuthStore, type UserCredentials } from '@/stores/auth.store';
import { reactive } from 'vue';
import { useRouter } from 'vue-router';

const authStore = useAuthStore();
const router = useRouter();

const credentials = reactive<UserCredentials>({
    login: '',
    password: '',
});

async function authorize() {
    await authStore.login(credentials);
    router.push('/');
}
</script>
