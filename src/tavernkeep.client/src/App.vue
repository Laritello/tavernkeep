<template>
  <div v-if="!userStore.getters.isLoggedIn" class="d-grid gap-2 col-6 mx-auto">
    <LoginForm />
  </div>
  <div v-else class="text-center">
    <h2>Welcome!</h2>
    <CounterField />
    <button class="btn btn-secondary" @click="userStore.logout()">
      Logout
    </button>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted } from 'vue';
import CounterField from './components/CounterField.vue';
import LoginForm from './components/LoginForm.vue';
import userStore from './stores/userStore';

export default defineComponent({
  components: { CounterField, LoginForm },
  setup() {
    onMounted(userStore.checkAuthState)
    return { userStore }
  }
})
</script>

<style scoped></style>
