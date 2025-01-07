<script setup lang="ts">
import type { SkillRollMessage } from '@/entities';

const { message } = defineProps<{ message: SkillRollMessage }>();
</script>

<template>
    <div class="flex flex-row items-center justify-center gap-4">
        <div class="flex flex-col">
            <span class="text-xl font-bold">CHECK</span>
            <span>{{ message.skill.type }}</span>
        </div>
        <div class="flex flex-col grow">
            <span class="self-end text-4xl font-bold">{{ message.result.value }}</span>
        </div>
    </div>
</template>

<style scoped>

</style>