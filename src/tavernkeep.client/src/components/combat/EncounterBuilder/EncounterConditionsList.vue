<script setup lang="ts">
import type { Condition } from '@/entities';
import { useConditionsStore } from '@/stores/useConditionsStore.ts';

const conditionsStore = useConditionsStore();

function onDragStart(event: DragEvent, item: Condition) {
    if (event.dataTransfer === null) {
        return;
    }

    event.dataTransfer.dropEffect = 'copy';
    event.dataTransfer.effectAllowed = 'copy';
    event.dataTransfer.setData('condition', item.name);
}
</script>

<template>
    <ul class="overflow-auto grid gap-1 justify-items-center">
        <li
            v-for="condition in conditionsStore.conditions"
            :key="condition.name"
            class="w-2/3 text-center cursor-default border-2 border-base-100 hover:border-primary rounded-md"
            draggable="true"
            @dragstart="onDragStart($event, condition)"
        >
            {{ condition.name }}
        </li>
    </ul>
</template>

<style scoped></style>
