<template>
    <v-select
        v-model="selected"
        :items="users"
        item-title="login"
        label="private message recipient..."
        density="compact"
        clearable
        class="-mb-5"
    />
</template>

<script setup lang="ts">
import type { User } from '@/entities/User';

const selected = defineModel<User>();
const { users } = defineProps<{ users: User[] }>();
</script>
