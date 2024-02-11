import { ApiClientFactory } from '@/factories/ApiClientFactory'
import { computed, reactive } from 'vue'

interface UserData {
    jwt: string | undefined,
    error: string | undefined
}

const state : UserData = reactive({ 
    jwt: undefined,
    error: undefined
})

const getters = reactive({
    isLoggedIn : computed(() => state.jwt != undefined)
})

const actions = {
    async login(login: string, password: string) {
        // Reset previous saved state
        state.jwt = undefined;
        state.error = undefined;

        // Call API for JWT
        const client = ApiClientFactory.createApiClient();
        const response = await client.auth(login, password);

        // If something went wrong - show error
        if (!response.isSuccess()) {
            state.error = response.statusText;
            return;
        }

        state.jwt = response.data;
    },

    async logout() {
        state.jwt = undefined;
    },

    checkAuthState() : boolean {
        return state.jwt != undefined;
    }
}

export { type UserData }
export default { state, getters, ...actions }