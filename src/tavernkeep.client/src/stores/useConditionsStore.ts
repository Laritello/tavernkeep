import { acceptHMRUpdate, defineStore } from 'pinia';
import { ref } from 'vue';

import type { Condition } from '@/entities';
import { ApiClientFactory } from '@/factories/ApiClientFactory.ts';

export const useConditionsStore = defineStore('conditions', () => {
    const api = ApiClientFactory.createApiClient();
    const conditions = ref<Condition[]>([]);

    async function fetch() {
        conditions.value = await api.getConditions();
    }

    return { conditions, fetch };
});

if (import.meta.hot) {
    import.meta.hot.accept(acceptHMRUpdate(useConditionsStore, import.meta.hot));
}
