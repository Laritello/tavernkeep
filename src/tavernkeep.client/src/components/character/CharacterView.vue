<template>
    <AbilitiesView v-if="currentUser" :abilities="currentUser.activeCharacter.abilities" />
    <h3 v-else>No active user</h3>
</template>
<script setup lang="ts">
import { useAppStore } from '@/stores/app.store';
import AbilitiesView from './AbilitiesView.vue';
import { storeToRefs } from 'pinia';

const appStore = useAppStore();
const { current: currentUser } = storeToRefs(appStore.users);
</script>
