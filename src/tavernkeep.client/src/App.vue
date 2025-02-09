<template>
    <ApplicationLoader>
        <RouterLayout />
        <ModalsProvider />
    </ApplicationLoader>
</template>

<script setup lang="ts">
import ApplicationLoader from '@/ApplicationLoader.vue';
import ModalsProvider from '@/components/dialogs/ModalsProvider.vue';
import RouterLayout from '@/layouts/RouterLayout.vue';
</script>
