<template>
    <div class="weather-component">
        <h1>Users</h1>
        <p>This component demonstrates fetching data from the server.</p>

        <div v-if="loading" class="loading">
            Loading... Please refresh once the ASP.NET backend has started. See <a
                href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
        </div>

        <div v-if="post" class="content">
            <table>
                <thead>
                    <tr>
                        <th>Login</th>
                        <th>Role</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="user in post" :key="user.id">
                        <td>{{ user.login }}</td>
                        <td>{{ user.role }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { ApiClientFactory } from '@/factories/ApiClientFactory';

let api = ApiClientFactory.createApiClient();

interface Data {
    loading: boolean,
    post: null | User[],
}

export default defineComponent({
    data(): Data {
        return {
            loading: false,
            post: null,
        };
    },
    mounted() {

        // fetch the data when the view is created and the data is
        // already being observed
        this.fetchData();
    },
    watch: {
        // call again the method if the route changes
        '$route': 'fetchData'
    },
    methods: {
        fetchData(): void {
            this.post = null;
            this.loading = true;

            api.getUsers()
                .then(r => {
                    this.post = r.data;
                    this.loading = false;
                    return;
                });
        },
    },
});
</script>

<style scoped>
th {
    font-weight: bold;
}

tr:nth-child(even) {
    background: #F2F2F2;
}

tr:nth-child(odd) {
    background: #FFF;
}

th,
td {
    padding-left: .5rem;
    padding-right: .5rem;
}

.weather-component {
    text-align: center;
}

table {
    margin-left: auto;
    margin-right: auto;
}</style>@/api/factories/ApiClientFactory@/factories/ApiClientFactory