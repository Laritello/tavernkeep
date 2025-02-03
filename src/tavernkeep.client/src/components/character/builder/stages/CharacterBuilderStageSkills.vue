<template>
    <ProficiencyListEdit v-model="skills" locale-prefix="pf.skills." @updated="update" />
</template>

<script setup lang="ts">
import type { BaseSkill, Skill } from '@/contracts/character';
import type { Character } from '@/entities';
import type { KeyValue } from '@/types';

import ProficiencyListEdit from '../../shared/ProficiencyListEdit/ProficiencyListEdit.vue';

const { character } = defineProps<{
    character: Character;
}>();

const skills = [...character.skills];

const emits = defineEmits<{
    update: [value: KeyValue<Character>];
}>();

function update(skills: BaseSkill[]) {
    emits('update', { key: 'skills', value: skills as Skill[] });
}
</script>
