<template>
    <div class="card card-body bg-neutral text-neutral-content">
        <h1 class="card-title">{{ name }}</h1>
        <div class="flex gap-2">
            <div>HP: {{ health.current }}/{{ health.max }}</div>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { Health } from '@/contracts/character';

defineProps<{
    name: string;
    health: Health;
}>();
</script>
