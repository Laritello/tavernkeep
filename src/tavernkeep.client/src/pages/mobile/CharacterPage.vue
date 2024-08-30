<script setup lang="ts">
import SkillsWidget from '@/components/character/SkillsWidget.vue';
import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';
import AbilitiesView from '@/components/character/AbilitiesView.vue';

const user = useCurrentUserAccount();
const character = user.activeCharacter;
</script>

<template>
    <div v-if="character !== undefined" class="flex flex-wrap max-h-full overflow-y-auto p-2 gap-2">
        <AbilitiesView :abilities="character.abilities" />
        <SkillsWidget :skills="character.skills" />
    </div>
    <div v-else>No selected character</div>
</template>

<style scoped></style>
