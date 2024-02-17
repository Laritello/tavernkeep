<template>
    <div v-if="!authStore.isLoggedIn" class="d-grid gap-2 col-6 mx-auto">
        <LoginForm />
    </div>
    <div v-else>
        <RoomComponent />
    </div>
</template>

<script setup lang="ts">
// import { defineComponent, onMounted } from 'vue';
import RoomComponent from './components/RoomComponent.vue';
import LoginForm from './components/LoginForm.vue';
import { useAuthStore } from './stores/auth.store';

const authStore = useAuthStore();
</script>

<style scoped></style>
./stores/auth.store