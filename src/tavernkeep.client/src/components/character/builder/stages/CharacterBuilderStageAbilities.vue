<template>
    <div class="flex flex-col items-center content-stretch">
        <label v-for="ability in template.abilities" :key="ability.name" class="form-control w-full">
            <label class="label">
                <span class="label-text">{{ t(`pf.attributes.${ability.name.toLowerCase()}`) }}</span>
            </label>
            <input v-model="ability.score" type="number" placeholder="Type here" class="input input-bordered w-full" />
        </label>
    </div>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia';
import { useI18n } from 'vue-i18n';

import { useCharacterBuilderStore } from '@/components/character/builder/stores/characterBuilderStore.ts';

const { t } = useI18n();
const characterBuilderStore = useCharacterBuilderStore();
const { template } = storeToRefs(characterBuilderStore);
</script>
