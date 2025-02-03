<template>
    <ProficiencyListEdit v-model="savingThrows" locale-prefix="pf.savingThrows." @updated="update" />
</template>

<script setup lang="ts">
import type { BaseSkill, SavingThrow } from '@/contracts/character';
import type { Character } from '@/entities';
import type { KeyValue } from '@/types';

import ProficiencyListEdit from '../../shared/ProficiencyListEdit/ProficiencyListEdit.vue';

const { character } = defineProps<{
    character: Character;
}>();

const savingThrows = [...character.savingThrows];

const emits = defineEmits<{
    update: [value: KeyValue<Character>];
}>();

function update(skills: BaseSkill[]) {
    emits('update', { key: 'savingThrows', value: skills as SavingThrow[] });
}
</script>
