<template>
    <div class="flex flex-col gap-2">
        <AbilityView
            v-for="ability in abilities"
            :key="ability.type.toString()"
            :ability="ability"
            @edit="$emit('edit', ability)"
        />
    </div>
</template>
<script setup lang="ts">
import type { Ability } from '@/contracts/character';

import AbilityView from './AbilityView.vue';
import type { AbilityType } from '@/contracts/enums';

const { abilities } = defineProps<{ abilities: Record<AbilityType, Ability> }>();
</script>
