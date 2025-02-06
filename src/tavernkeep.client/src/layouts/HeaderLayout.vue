<template>
    <div class="flex flex-col h-dvh">
        <header class="sticky top-0 flex border-b-2 border-base-100 items-center bg-base-100 shadow-md">
            <button class="btn btn-circle btn-ghost btn-md" @click="goBack">
                <span class="mdi mdi-arrow-left text-xl"></span>
            </button>
            <div>
                <h1 class="mx-2 text-xl font-semibold leading-4">{{ header.title }}</h1>
                <h2 class="mx-2 text-base-content/50 text-xs font-normal leading-4">{{ header.subtitle }}</h2>
            </div>
        </header>

        <main class="container mx-auto p-2">
            <slot />
        </main>
    </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router';

import { useHeaderStore } from '@/stores/header';

const header = useHeaderStore();
const router = useRouter();

async function goBack() {
    if (window.history.state.back === null) {
        await router.push({ path: '/' });
    } else {
        router.back();
    }
}
</script>
