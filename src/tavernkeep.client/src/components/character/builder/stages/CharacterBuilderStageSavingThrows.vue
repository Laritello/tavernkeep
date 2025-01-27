<template>
    <ProficiencyListEdit locale-prefix="pf.savingThrows." v-model="savingThrows" />
</template>

<script setup lang="ts">
import type { Character } from '@/entities';
import ProficiencyListEdit from '../../shared/ProficiencyListEdit/ProficiencyListEdit.vue';
import type { BaseSkill } from '@/contracts/character';

const { character } = defineProps<{
    character: Character
}>();

const savingThrows = Object
    .values(character.savingThrows)
    .map(item => ({ name: item.name.toString(), proficiency: item.proficiency, type: item.type } as BaseSkill));
</script>