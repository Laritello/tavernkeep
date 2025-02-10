<template>
    <div class="mx-auto">
        <CharacterComponentOld v-if="activeCharacter" :character="activeCharacter" />
        <span v-else>No active character</span>
    </div>
</template>

<script setup lang="ts">
import CharacterComponentOld from '@/components/character/CharacterComponentOld.vue';
import { useSession } from '@/composables/useSession';
import { useUserAccount } from '@/composables/useUserAccount';

const session = useSession();
const userAccount = useUserAccount(session.userId);
const activeCharacter = userAccount.activeCharacter;
</script>
