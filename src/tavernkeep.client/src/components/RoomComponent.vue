<template>
  <div class="container">
    <div class="row">
      <div class="col-sm" style="background-color: darkgoldenrod;">
        <UserList :users="users"></UserList>
      </div>
      <div>
        <h2>Chat Window</h2>
        <div class="messages">
          <div v-for="message in messages" :key="message.id">
            <strong>{{ message.sender }}:</strong> {{ message.text }}
          </div>
        </div>
        <div class="input-box">
          <input v-model="newMessage" type="text" placeholder="Type your message..." />
          <button @click="sendMessage">Send</button>
        </div>
      </div>
    </div>
  </div>
</template>
  
<script lang="ts">
import { User } from '@/entities/User';
import UserList from './UserList.vue';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { ApiClient } from '@/api/base/ApiClient';
import userStore from '@/stores/userStore';

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
  components: { UserList },
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