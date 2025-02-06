<script setup lang="ts">
import BottomNavigationView from '@/components/BottomNavigationView.vue';
import TopNavigationView from '@/components/TopNavigationView.vue';
</script>

<template>
    <div class="flex flex-col h-dvh">
        <TopNavigationView class="sticky top-0" />

        <div class="grow bg-base-200 overflow-y-auto">
            <slot />
        </div>

        <BottomNavigationView class="sticky bottom-0 min-h-12 max-h-12 lg:hidden" />
    </div>
</template>

<style scoped></style>
