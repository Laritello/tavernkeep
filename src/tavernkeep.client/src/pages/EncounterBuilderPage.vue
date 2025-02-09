<template>
    <div class="flex flex-col bg-base-200 h-dvh">
        <EncounterBuilderHeader class="bg-base-100 h-12" />
    </div>
</template>

<script setup lang="ts">
import EncounterBuilderHeader from '@/components/combat/EncounterBuilder/EncounterBuilderHeader.vue';
</script>

<!--suppress CssUnusedSymbol -->
<style scoped>
.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.15s ease;
}

.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}
</style>
