<template>
    <div class="w-full h-full">
        <CharacterView v-if="activeCharacter" :character="activeCharacter" />
        <span v-else>No active character</span>
    </div>
</template>

<script setup lang="ts">
import CharacterView from '@/components/character/CharacterView.vue';

import { storeToRefs } from 'pinia';
import { useAppStore } from '@/stores/app.store';
import { useActiveCharacter } from '@/composables/useCharacter';

const appStore = useAppStore();
const { currentUserId } = storeToRefs(appStore.auth);
const { character: activeCharacter } = useActiveCharacter(currentUserId.value);
</script>
