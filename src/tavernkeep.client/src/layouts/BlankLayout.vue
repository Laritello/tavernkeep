<template>
    <header class="sticky top-0 flex items-center bg-base-200 shadow-md">
        <button @click="goBack"
                class="btn btn-circle btn-ghost btn-md">
            <span class="mdi mdi-arrow-left text-xl"></span>
        </button>
        <h1 class="mx-2 text-xl font-semibold">{{ title }}</h1>
    </header>
    <div class="container mx-auto p-4">
        <slot />
    </div>
</template>

<script setup lang="ts">
import { useRouter, useRoute } from 'vue-router';

const router = useRouter();
const route = useRoute();
const title = route.meta.title;

function goBack() {
    if (window.history.state.back === null) {
        router.push({ path: '/' });
    } else {
        router.back();
    }
}
</script>