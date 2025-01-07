<script setup lang="ts">
import BottomNavigationView from '@/components/BottomNavigationView.vue';
import TopNavigationView from '@/components/TopNavigationView.vue';
</script>

<template>
    <div class="flex flex-col">
        <TopNavigationView class="sticky top-0" />

        <div class="h-full overflow-hidden">
            <slot />
        </div>

        <BottomNavigationView class="sticky bottom-0 lg:hidden" />
    </div>
</template>

<style scoped></style>
