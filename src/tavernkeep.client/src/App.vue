<template>
    <component :is="layout">
        <router-view />
    </component>
    <ModalsProvider />
</template>

<script setup lang="ts">
import { shallowRef, provide, type Component } from 'vue';
import { useRouter } from 'vue-router';

import layouts from '@/layouts';

import ModalsProvider from './components/dialogs/ModalsProvider.vue';

const layout = shallowRef<Component>();
const router = useRouter();
provide('app:layout', layout);

router.afterEach((to) => {
    layout.value = layouts[to.meta.layout || 'BlankLayout'];
});
</script>

<style scoped></style>
