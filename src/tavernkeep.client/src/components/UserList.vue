<template>
  <div>
    <h2>Create user</h2>
    <div>
      <form @submit.prevent="createUser">
        <div class="form-group my-2">
          <label>Login</label>
          <input v-model="form.login" class="form-control" placeholder="Login" required>
        </div>
        <div class="form-group my-2">
          <label>Password</label>
          <input v-model="form.password" class="form-control" placeholder="Login" required>
        </div>
        <button type="submit">Create</button>
      </form>
    </div>
    <h2>Existing users</h2>
    <ul>
      <li v-for="user in users" :key="user.id">
        {{ user.login }}
        <button>delete</button>
      </li>
    </ul>
  </div>
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