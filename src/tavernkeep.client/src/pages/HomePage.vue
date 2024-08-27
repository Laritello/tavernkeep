<template>
    <div class="mx-auto ">
        <CharacterComponent v-if="activeCharacter" :character="activeCharacter" />
        <span v-else>No active character</span>
    </div>
</template>

<script setup lang="ts">
import CharacterComponent from '@/components/character/CharacterComponent.vue';
import { useSession } from '@/composables/useSession';
import { useUserAccount } from '@/composables/useUserAccount';

const session = useSession();
const userAccount = useUserAccount(session.userId);
const activeCharacter = userAccount.activeCharacter;
</script>
