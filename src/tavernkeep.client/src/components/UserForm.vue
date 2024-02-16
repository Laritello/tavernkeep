<template>
    <v-sheet class="mx-auto">
        <v-form fast-fail @submit.prevent="createUser" class="pa-2">
            <div class="text-h6">Create</div>
            <v-text-field v-model="model.login" label="Login" :rules="model.loginRules"></v-text-field>

            <v-text-field v-model="model.password" label="Password" :rules="model.passwordRules"></v-text-field>

            <v-combobox v-model="model.role" label="Role"
                :items="[UserRole.Master, UserRole.Moderator, UserRole.Player]"></v-combobox>

            <v-btn type="submit" block>Create</v-btn>
        </v-form>
    </v-sheet>
</template>

<script setup lang="ts">
import { reactive } from 'vue';
import { UserRole } from '@/contracts/enums/UserRole';

import { useRoomUsersStore } from '@/stores/roomUsersStore';

const roomUsersStore = useRoomUsersStore();

interface UserData {
    login: string,
    password: string,
    role: UserRole,
    loginRules: any[] | undefined,
    passwordRules: any[] | undefined
}

const model = reactive<UserData>({
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
})

async function createUser() {
    roomUsersStore.createUser(model.login, model.password, model.role);
}
</script>