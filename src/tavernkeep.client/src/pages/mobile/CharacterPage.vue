<script setup lang="ts">
import SkillsView from '@/components/character/SkillsView.vue';
import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';
import AbilitiesView from '@/components/character/AbilitiesView.vue';

const user = useCurrentUserAccount();
const character = user.activeCharacter;
</script>

<template>
    <div v-if="character !== undefined" class="flex overflow-y-auto max-h-full p-2 gap-2 flex-wrap">
        <AbilitiesView :abilities="character.abilities" />
        <SkillsView :skills="character.skills" />
    </div>
    <div v-else>No selected character</div>
</template>

<style scoped></style>
