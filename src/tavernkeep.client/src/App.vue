<template>
    <component :is="layout">
        <router-view />
    </component>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router';
const router = useRouter();

import { shallowRef, provide, type Component } from 'vue';
import layouts from '@/layouts';

const layout = shallowRef<Component>();
provide('app:layout', layout);

router.afterEach((to) => {
    layout.value = layouts[to.meta.layout || 'BlankLayout'];
});
</script>

<style scoped></style>
