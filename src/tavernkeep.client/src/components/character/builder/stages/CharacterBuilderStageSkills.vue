<template>
    <ProficiencyListEdit locale-prefix="pf.skills." v-model="skills" />
</template>

<script setup lang="ts">
import type { Character } from '@/entities';
import ProficiencyListEdit from '../../shared/ProficiencyListEdit/ProficiencyListEdit.vue';
import type { BaseSkill } from '@/contracts/character';

const { character } = defineProps<{
    character: Character
}>();

const skills = Object
    .values(character.skills)
    .map(item => ({ name: item.name.toString(), proficiency: item.proficiency, type: item.type } as BaseSkill));
</script>