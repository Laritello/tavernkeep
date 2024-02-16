<template>
  <v-layout class="rounded rounded-md">
    <v-app-bar color="primary" title="Tavernkeep"></v-app-bar>

    <v-navigation-drawer>
      <v-col>
        <v-card>
          <UserForm />
        </v-card>
        <v-card class="mt-3">
          <UserList :users="roomUsersStore.users" />
        </v-card>
      </v-col>
    </v-navigation-drawer>

    <v-navigation-drawer location="right" width="300" permanent>
      <ChatComponent style="max-height: 100%;">

      </ChatComponent>
    </v-navigation-drawer>

    <v-main class="d-flex align-center justify-center" style="min-height: 300px;">
      Main Content
    </v-main>
  </v-layout>
</template>
  
<script setup lang="ts">
import UserList from './UserList.vue';
import UserForm from './UserForm.vue';
import ChatComponent from './ChatComponent.vue';
import { useRoomUsersStore } from '@/stores/roomUsersStore';
// import { onMounted } from 'vue';

const roomUsersStore = useRoomUsersStore();
roomUsersStore.fetchUsers();
</script>@/stores/roomUserStore