<template>
    <div class="card card-body bg-neutral text-neutral-content w-full max-h-72 lg:max-w-md">
        <h1 class="card-title">Abilities</h1>
        <div class="flex flex-col gap-1">
            <AbilityView
                v-for="ability in abilities"
                :key="ability.type.toString()"
                :ability="ability"
                @edit="$emit('edit', ability)"
            />
        </div>
    </div>
</template>
<script setup lang="ts">
import type { Ability } from '@/contracts/character';

import AbilityView from './AbilityView.vue';
import type { AbilityType } from '@/contracts/enums';

const { abilities } = defineProps<{ abilities: Record<AbilityType, Ability> }>();
</script>
