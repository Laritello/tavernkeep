<template>
    <div class="flex flex-col items-center content-stretch">
        <label v-for="ability in abilities" :key="ability.name" class="form-control w-full">
            <div class="label">
                <span class="label-text">{{ t(`pf.attributes.${ability.name.toLowerCase()}`) }}</span>
            </div>
            <input type="text" placeholder="Type here" class="input input-bordered w-full"
                v-model="ability.score" />
        </label>
    </div>
</template>

<script setup lang="ts">
import type { Ability } from '@/contracts/character';
import type { Character } from '@/entities';
import { watch } from 'vue';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();

const { character } = defineProps<{
    character: Character
}>();

const abilities: Ability[] = [...character.abilities];

const emits = defineEmits<{
    update: [value: { key: keyof Character; value: unknown }]
}>();

watch(abilities, () => emits('update', { key: 'abilities', value: abilities }));

</script>