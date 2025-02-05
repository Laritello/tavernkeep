<script setup lang="ts">
import { useRouter } from 'vue-router';

import CharacterBuilder from '@/components/character/builder/CharacterBuilder.vue';
import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount.ts';
import type { Character } from '@/entities';

const user = useCurrentUserAccount();
const router = useRouter();

async function createCharacter(template: Character) {
    const character = await user.createCharacter(template);

    if (character !== undefined) {
        await router.push('/settings');
    }
}
</script>

<template>
    <CharacterBuilder @complete="createCharacter" />
</template>

<style scoped></style>
