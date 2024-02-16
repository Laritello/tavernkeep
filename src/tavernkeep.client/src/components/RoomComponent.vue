<template>
  <v-layout class="rounded rounded-md">
    <v-app-bar color="primary" title="Tavernkeep"></v-app-bar>

    <v-navigation-drawer>
      <v-col>
        <v-card>
          <UserForm />
        </v-card>
        <v-card class="mt-3">
          <UserList />
        </v-card>
      </v-col>
    </v-navigation-drawer>

    <v-navigation-drawer location="right" width="300" permanent>
      <ChatComponent style="max-height: 100%;">

      </ChatComponent>
    </v-navigation-drawer>

    <v-main class="d-flex align-center justify-center" style="min-height: 300px;">
      <PartyComponent />
    </v-main>
  </v-layout>
</template>
  
<script setup lang="ts">
import { onMounted } from 'vue';
import UserForm from '@/components/UserForm.vue'
import UserList from '@/components/UserList.vue'
import ChatComponent from '@/components/ChatComponent.vue'
import { useRoomUsersStore } from '@/stores/roomUsersStore';
import PartyComponent from './PartyComponent.vue';

const roomUsersStore = useRoomUsersStore();
onMounted(async () => await roomUsersStore.fetchUsers());
</script>