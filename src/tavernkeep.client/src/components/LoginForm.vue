<template>
    <form @submit.prevent="onSubmit">
        <div class="form-group my-2">
            <label>Login</label>
            <input v-model="form.login" class="form-control" placeholder="Login" required>
        </div>
        <div class="form-group my-2">
            <label>Password</label>
            <input v-model="form.password" class="form-control" placeholder="Login" required>
        </div>
        <div class="text-danger my-2"> {{ userStore.state.error }} </div>
        <button type="submit">Login</button>
    </form>
</template>
  
<script lang="ts">
import { defineComponent, reactive } from 'vue'
import userStore from '@/stores/userStore'

export default defineComponent({
    setup() {
        const form = reactive({
            login: '',
            password: '',
        })

        const onSubmit = () => {
            userStore.login(form.login, form.password)
            form.login = ''
            form.password = ''
        }

        return { form, userStore, onSubmit }
    }
});
</script>