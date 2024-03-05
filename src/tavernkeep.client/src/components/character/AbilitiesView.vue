<template>
    <div class="grid grid-cols-2 gap-x-2 gap-y-3">
        <AbilityView v-for="[key, ability] in userCharacter?.abilities" :key="key.toString()" :ability="ability" />
    </div>
</template>
<script setup lang="ts">
import { useUsersStore } from '@/stores/users.store';
import AbilityView from './AbilityView.vue';

const users = useUsersStore();
const currentUser = users.currentUser;
const userCharacter = currentUser?.activeCharacter;
console.log(userCharacter);
</script>
