<template>
    <v-sheet class="mx-auto">
        <v-form fast-fail @submit.prevent="createUser" class="pa-2">
            <div class="text-h6">Create</div>
            <v-text-field v-model="login" label="Login" :rules="loginRules"></v-text-field>

            <v-text-field v-model="password" label="Password" :rules="passwordRules"></v-text-field>

            <v-combobox v-model="role" label="Role" :items="[UserRole.Master, UserRole.Moderator, UserRole.Player]"></v-combobox>

            <v-btn type="submit" block>Create</v-btn>
        </v-form>
    </v-sheet>
</template>

<script lang="ts">
import type { ApiClient } from '@/api/base/ApiClient';
import { UserRole } from '@/contracts/enums/UserRole';
import { ApiClientFactory } from '@/factories/ApiClientFactory';

const client: ApiClient = ApiClientFactory.createApiClient();

interface UserData {
    login: string,
    password: string,
    role: UserRole,
    loginRules: any[] | undefined,
    passwordRules: any[] | undefined
}

export default {
    setup() {
        return { UserRole }
    },
    data: (): UserData => ({
        login: '',
        password: '',
        role: UserRole.Player,
        loginRules: [
            (value: string) => {
                if (value) return true
                return 'You must enter a login.'
            },
        ],
        passwordRules: [
            (value: string) => {
                if (value) return true
                return 'You must enter a password.'
            },
        ],
    }),
    methods: {
        async createUser() {
            await client.createUser(this.login, this.password, this.role)
        },
    }
}
</script>