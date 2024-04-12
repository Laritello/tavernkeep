<template>
    <div class="w-full h-full">
        <CharacterView v-if="activeCharacter" :character="activeCharacter" />
        <span v-else>No active character</span>
    </div>
</template>

<script setup lang="ts">
import CharacterView from '@/components/character/CharacterView.vue';
import { useSession } from '@/composables/useSession';
import { useUserAccount } from '@/composables/useUserAccount';

const session = useSession();
const userAccount = useUserAccount(session.userId);
const activeCharacter = userAccount.activeCharacter;
</script>
