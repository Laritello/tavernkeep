<template>
  <v-sheet class="mx-auto pa-2">
    <div class="text-h6">Users</div>
    <v-list>
      <v-list-item v-for="user in users" :key="user.id" v-bind:title="user.login" />
    </v-list>
  </v-sheet>
</template>

<script lang="ts">
import type { ApiClient } from '@/api/base/ApiClient';
import { UserRole } from '@/contracts/enums/UserRole';
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
    async createUser() {
      const response = await client.createUser(form.login, form.password, UserRole.Player)
      if (response.isSuccess()) {
        this.$props.users.push(response.data)
      }
    }
  }
})
</script>