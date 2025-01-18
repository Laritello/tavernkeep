<template>
    <v-sheet class="mx-auto pa-2">
        <div class="text-h6">Users</div>
        <v-list>
            <v-list-item v-for="user in usersStore.dictionary" :key="user.id" v-bind:title="user.login">
                <template v-slot:append>
                    <v-btn size="small" variant="text" icon="mdi-square-edit-outline" @click="editDialog = true" />
                    <v-btn size="small" variant="text" icon="mdi-delete" @click="usersStore.deleteUser(user.id)" />
                </template>
            </v-list-item>
        </v-list>
    </v-sheet>

    <v-dialog v-model="editDialog" width="auto">
        <v-card max-width="400" prepend-icon="mdi-square-edit-outline" title="User edit">
            <UserEdit v-model="editUserModel" />
            <template v-slot:actions>
                <v-btn class="ms-auto" text="Edit" @click="editDialog = false"></v-btn>
            </template>
        </v-card>
    </v-dialog>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useUsers } from '@/stores/users';
import UserEdit from './UserEdit.vue';
import { reactive } from 'vue';
import { UserRole } from '@/contracts/enums/UserRole';

const usersStore = useUsers();
const editDialog = ref(false);
const editUserModel = reactive({
    login: '',
    password: '',
    role: UserRole.Player,
});
</script>
