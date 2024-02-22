<template>
    <div class="container m-4">
        <h1 class="text-xl">GM Panel</h1>
        <div class="w-64 flex flex-col gap-4">
            <v-card>
                <UserForm />
            </v-card>
            <v-card>
                <UserList />
            </v-card>
        </div>
    </div>
</template>

<script setup lang="ts">
import UserForm from '@/components/UserForm.vue';
import UserList from '@/components/UserList.vue';
</script>
