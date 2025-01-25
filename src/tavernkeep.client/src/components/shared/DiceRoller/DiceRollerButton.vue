<script setup lang="ts">
import { ref } from 'vue';
import D20 from '@/assets/dice/d20-grey.svg';

const open = ref(false);
</script>

<template>
    <div>
        <D20 v-if="!open" class="fill-current size-8" />
        <span v-else class="mdi mdi-close"></span>
    </div>
</template>

<style scoped></style>
