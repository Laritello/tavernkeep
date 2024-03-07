<template>
    <div class="grid grid-cols-2 gap-x-2 gap-y-3">
        <AbilityView
            v-for="[key, ability] in appStore.users.current?.activeCharacter.abilities.entries()"
            :key="key.toString()"
            :ability="ability"
        />
    </div>
</template>
<script setup lang="ts">
import { useAppStore } from '@/stores/app.store';

import AbilityView from './AbilityView.vue';

const appStore = useAppStore();
</script>
