<template>
    <div class="flex flex-col items-center content-stretch">
        <label v-for="ability in abilities" :key="ability.name" class="form-control w-full max-w-xs">
            <div class="label">
                <span class="label-text">{{ t(`pf.attributes.${ability.name.toLowerCase()}`) }}</span>
            </div>
            <input type="text" placeholder="Type here" class="input input-bordered w-full max-w-xs"
                v-model="ability.score" />
        </label>
    </div>
</template>

<script setup lang="ts">
import type { Character } from '@/entities';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();

const { character } = defineProps<{
    character: Character
}>();

const abilities = Object
    .values(character.abilities)
    .map(item => ({ name: item.name.toString(), score: item.score }));
</script>