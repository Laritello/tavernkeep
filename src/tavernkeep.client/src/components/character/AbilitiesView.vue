divdiv
<template>
    <div class="flex flex-wrap justify-center gap-2">
        <AbilityView
            v-for="ability in appStore.users.current?.activeCharacter.abilities"
            v-model="ability.score"
            :key="ability.type.toString()"
            :type="ability.type"
        />
    </div>
</template>
<script setup lang="ts">
import { useAppStore } from '@/stores/app.store';

import AbilityView from './AbilityView.vue';

const appStore = useAppStore();
</script>
