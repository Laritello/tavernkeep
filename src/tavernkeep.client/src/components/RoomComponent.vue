<template>
  <v-layout class="rounded rounded-md">
    <v-app-bar color="surface-variant" title="Tavernkeep"></v-app-bar>

    <v-navigation-drawer>
      <v-col>
        <v-card>
          <UserForm />
        </v-card>
        <v-card class="mt-3">
          <UserList :users="users"/>
        </v-card>
      </v-col>
    </v-navigation-drawer>

    <v-navigation-drawer location="right">
      <v-list>
        <v-list-item title="Drawer right"></v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-main class="d-flex align-center justify-center" style="min-height: 300px;">
      Main Content
    </v-main>
  </v-layout>
</template>
  
<script lang="ts">
import { User } from '@/entities/User';
import UserList from './UserList.vue';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { ApiClient } from '@/api/base/ApiClient';
import userStore from '@/stores/userStore';
import UserForm from './UserForm.vue';

const client: ApiClient = ApiClientFactory.createApiClient();

interface Message {
  id: number,
  text: string,
  sender: string
}

interface RoomModel {
  users: User[],
  messages: Message[],
  newMessage: string
}

export default {
  components: { UserList, UserForm },
  data(): RoomModel {
    return {
      users: [],
      messages: [],
      newMessage: ''
    };
  },

  setup() {
    return { userStore }
  },

  async mounted() {
    const response = await client.getUsers();
    this.users = response.data;
  },

  methods: {
    sendMessage() {
      if (this.newMessage.trim() !== '') {
        const newMessage = {
          id: this.messages.length + 1,
          sender: 'User 1', // Assuming the sender is always User 1 for simplicity
          text: this.newMessage,
        };
        this.messages.push(newMessage);
        this.newMessage = '';
      }
    },
  },
};
</script>
  
<style scoped></style>