<template>
    <div class="w-full h-full">
        <CharacterView v-if="activeCharacter" :character="activeCharacter" />
        <span v-else>No active character</span>
    </div>
</template>

<script setup lang="ts">
import CharacterView from '@/components/character/CharacterView.vue';
import type { Character } from '@/entities';

import { useAppStore } from '@/stores/app.store';
import { ref, watchEffect } from 'vue';

const appStore = useAppStore();
const activeCharacter = ref<Character>();

watchEffect(() => {
    const currentUser = appStore.users.currentUser;
    if (!currentUser) return;
    activeCharacter.value = appStore.characters.get(currentUser.activeCharacterId);
});
</script>
