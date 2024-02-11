<template>
  <div v-if="!userStore.getters.isLoggedIn" class="d-grid gap-2 col-6 mx-auto">
    <LoginForm />
  </div>
  <div v-else>
    <RoomComponent />
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted } from 'vue';
import RoomComponent from './components/RoomComponent.vue';
import LoginForm from './components/LoginForm.vue';
import userStore from './stores/userStore';

export default defineComponent({
  components: { RoomComponent, LoginForm },
  setup() {
    onMounted(userStore.checkAuthState)
    return { userStore }
  },
})
</script>

<style scoped></style>
