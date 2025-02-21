import { defineStore } from 'pinia';
import { ref } from 'vue';

import type { CreatureShort } from '@/contracts/creatures/CreatureShort.ts';
import { ApiClientFactory } from '@/factories/ApiClientFactory.ts';

export const useCreaturesStore = defineStore('creatures', () => {
    const api = ApiClientFactory.createApiClient();
    const creatures = ref<CreatureShort[]>([]);
    async function fetch() {
        creatures.value = await api.getCreatureList();
    }

    return { creatures, fetch };
});
