<template>
    <component :is="layout">
        <router-view />
    </component>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router';
import { shallowRef, provide, type Component } from 'vue';
import layouts from '@/layouts';

// Stores
import { useCharactersStore } from '@/stores/characters.store';
import { useMessagesStore } from '@/stores/messages.store';
import { useUsersStore } from '@/stores/users.store';

const charactersStore = useCharactersStore();
const messagesStore = useMessagesStore();
const usersStore = useUsersStore();

// Fetch data from server
charactersStore.fetchCharacters();
messagesStore.fetchMessages(0, 20);
usersStore.fetchUsers();

const layout = shallowRef<Component>();
const router = useRouter();
provide('app:layout', layout);

router.afterEach((to) => {
    layout.value = layouts[to.meta.layout || 'BlankLayout'];
});
</script>

<style scoped></style>
