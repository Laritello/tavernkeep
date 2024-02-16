<template>
  <v-sheet class="mx-auto pa-2">
    <div class="text-h6">Users</div>
    <v-list>
      <v-list-item v-for="user in users" :key="user.id" v-bind:title="user.login">
        <template v-slot:append>
          <v-btn size="small" variant="text" icon="mdi-delete" @click="deleteUser(user)"></v-btn>
        </template>
      </v-list-item>
    </v-list>
  </v-sheet>
</template>

<script lang="ts">
import type { ApiClient } from '@/api/base/ApiClient';
import { User } from '@/entities/User';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import { reactive, defineComponent, type PropType } from 'vue'

const client: ApiClient = ApiClientFactory.createApiClient();

const form = reactive({
  login: '',
  password: '',
})

export default defineComponent({
  setup() {
    return { form }
  },

  props: {
    users: {
      type: Object as PropType<User[]>,
      required: true
    }
  },

  methods: {
    async deleteUser(user: User) {
      const response = await client.deleteUser(user.id)

      if (response.isSuccess()) {
        const index = this.$props.users.indexOf(user, 0);

        if (index > -1) {
          this.$props.users.splice(index, 1);
        }
      }
    }
  }
})
</script>