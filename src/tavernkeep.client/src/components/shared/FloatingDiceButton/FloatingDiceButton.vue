<script setup lang="ts">
import { ref } from 'vue';
import D20 from '@/assets/dice/d20-grey.svg';
import FloatingDiceMenu from '@/components/shared/FloatingDiceButton/FloatingDiceMenu.vue';

const open = ref(false);
</script>

<template>
    <FloatingDiceMenu v-if="open" class="fixed bottom-12 right-0 rounded-t-lg" />
    <div class="flex flex-row fixed bottom-28 right-1">
        <div @click="open = !open" class="btn btn-circle btn-primary btn-md">
            <D20 v-if="!open" class="fill-current size-8" />
            <span v-else class="mdi mdi-close"></span>
        </div>
    </div>
</template>

<style scoped></style>
