<template>
    <v-select
        v-model="selected"
        :items="users"
        item-title="login"
        item-value="id"
        label="private message recipient..."
        density="compact"
        clearable
        class="-mb-5"
    />
</template>

<script setup lang="ts">
import type { User } from '@/entities/User';

const selected = defineModel<string>();
const { users } = defineProps<{ users: User[] }>();
</script>
