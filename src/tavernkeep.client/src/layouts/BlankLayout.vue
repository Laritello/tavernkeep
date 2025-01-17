<template>
    <div>
        <header class="flex items-center bg-base-200 shadow-md">
            <button @click="goBack"
                    class="btn btn-circle btn-ghost btn-md">
                <span class="mdi mdi-arrow-left text-xl"></span>
            </button>
            <h1 class="mx-2 text-xl font-semibold">{{ title }}</h1>
        </header>
        <slot />
    </div>
</template>

<script setup lang="ts">
import { useRouter, useRoute } from 'vue-router';

const router = useRouter();
const route = useRoute();
const title = route.meta.title;

function goBack() {
    router.go(-1); // Navigate back to the previous page
}
</script>