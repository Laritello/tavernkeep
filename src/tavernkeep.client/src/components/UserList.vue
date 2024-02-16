<template>
  <v-sheet class="mx-auto pa-2">
    <div class="text-h6">Users</div>
    <v-list>
      <v-list-item v-for="user in roomUsersStore.users" :key="user.id" v-bind:title="user.login">
        <template v-slot:append>
          <v-btn size="small" variant="text" icon="mdi-delete" @click="roomUsersStore.deleteUser(user.id)" />
        </template>
      </v-list-item>
    </v-list>
  </v-sheet>
</template>

<script setup lang="ts">
import { useRoomUsersStore } from '@/stores/roomUsersStore';

const roomUsersStore = useRoomUsersStore();
</script>