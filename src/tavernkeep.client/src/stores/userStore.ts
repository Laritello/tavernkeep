import { ApiClientFactory } from '@/factories/ApiClientFactory'
import { computed, reactive } from 'vue'
import { getCookie, setCookie, removeCookie } from 'typescript-cookie'
import type { ApiClient } from '@/api/base/ApiClient';

// Interface declarations
interface UserData {
    isLoggedIn: boolean,
}

// Constants initializations
const client : ApiClient = ApiClientFactory.createApiClient();
const cookieName : string = 'taverkeep.auth.jwt';

const state : UserData = reactive({ 
    isLoggedIn: false
})

const getters = reactive({
    isLoggedIn : computed(() => checkLoginStatus(cookieName))
})

const actions = {
    async login(login: string, password: string) {
        if (state.isLoggedIn)
            this.logout()

        // Call API for JWT
        const response = await client.auth(login, password);

        // TODO: Error handling
        if (!response.isSuccess()) {
            return;
        }

        setCookie(cookieName, response.data, { expires: 7 })
        state.isLoggedIn = true;
    },

    async logout() {
        state.isLoggedIn = false;
        removeCookie(cookieName)
    },

    checkAuthState() : boolean {
        return checkLoginStatus(cookieName)
    }
}

function checkLoginStatus(name: string) : boolean {
    state.isLoggedIn = getCookie(name) != undefined
    return state.isLoggedIn 
}

export { type UserData }
export default { state, getters, ...actions }