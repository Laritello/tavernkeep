<template>
    <div class="flex flex-col h-full">
        <div class="sticky top-0 flex items-center bg-base-200 shadow-md">
            <button @click="goBack" class="btn btn-circle btn-ghost btn-md">
                <span class="mdi mdi-arrow-left text-xl"></span>
            </button>
            <div>
            <h1 class="mx-2 text-xl font-semibold">{{ header.title }}</h1>
            <h2 class="mx-2 text-xs font-semibold">{{ header.subtitle }}</h2>
        </div>
        </div>

        <div class="h-dvh overflow-hidden">
            <slot />
        </div>
    </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router';
import { useHeaderStore } from '@/stores/header';

const header = useHeaderStore();
const router = useRouter();

function goBack() {
    if (window.history.state.back === null) {
        router.push({ path: '/' });
    } else {
        router.back();
    }
}
</script>