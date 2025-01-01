<template>
    <component :is="layout">
        <router-view />
    </component>
    <ModalsProvider />
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router';
import { shallowRef, provide, type Component, onMounted } from 'vue';
import layouts from '@/layouts';
import ModalsProvider from './components/dialogs/ModalsProvider.vue';
import { themeChange } from 'theme-change';

const layout = shallowRef<Component>();
const router = useRouter();
provide('app:layout', layout);

router.afterEach((to) => {
    layout.value = layouts[to.meta.layout || 'BlankLayout'];
});

onMounted(() => {
    themeChange(true);
})

// Change the theme dynamically
const changeTheme = (event: Event): void => {
    const target = event.target as HTMLSelectElement;
    const selectedTheme = target.value;
    document.documentElement.setAttribute("data-theme", selectedTheme);
};

</script>

<style scoped></style>
