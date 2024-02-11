<template>
    <div class="d-flex align-center justify-center" style="height: 100vh">
        <v-sheet width="400" class="mx-auto">
            <v-form fast-fail @submit.prevent="authorize">
                <v-text-field v-model="login" label="User Name"></v-text-field>

                <v-text-field v-model="password" label="password"></v-text-field>
                <a href="#" class="text-body-2 font-weight-regular">Forgot Password?</a>

                <v-btn type="submit" color="primary" block class="mt-2">Sign in</v-btn>

            </v-form>
            <div class="mt-2">
                <p class="text-body-2">Don't have an account? <a href="#">Sign Up</a></p>
            </div>
        </v-sheet>
    </div>
</template>
  
<script lang="ts">
import userStore from '@/stores/userStore'

interface UserCredentials {
    login: string,
    password: string
}

export default {

    data: (): UserCredentials => ({
        login: '',
        password: '',
    }),
    methods: {
        authorize() {
            userStore.login(this.login, this.password)
            this.login = ''
            this.password = ''
        },
    },
}
</script>

