<template>
    <AbilitiesView :abilities="character.abilities" />
</template>
<script setup lang="ts">
import AbilitiesView from './AbilitiesView.vue';
import type { Character } from '@/entities';

defineProps<{
    character: Character;
}>();
</script>
