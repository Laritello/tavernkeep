<template>
    <div class="d-flex align-center justify-center" style="height: 100vh">
        <v-sheet width="400" class="mx-auto">
            <v-form fast-fail @submit.prevent="logout">
                <v-btn type="submit" color="primary" block class="mt-2">
                    Sign out
                </v-btn>
            </v-form>
        </v-sheet>
    </div>
</template>

<script setup lang="ts">
import { useAuthStore } from '@/stores/auth.store';
import { useRouter } from 'vue-router';
const auth = useAuthStore();
const router = useRouter();

async function logout() {
    await auth.logout();
    router.push('/login');
}
</script>
