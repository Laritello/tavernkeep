<template>
    <ProficiencyListEdit locale-prefix="pf.savingThrows." v-model="savingThrows" @updated="update" />
</template>

<script setup lang="ts">
import type { Character } from '@/entities';
import ProficiencyListEdit from '../../shared/ProficiencyListEdit/ProficiencyListEdit.vue';
import type { BaseSkill } from '@/contracts/character';

const { character } = defineProps<{
    character: Character
}>();

const savingThrows = [...character.savingThrows];

const emits = defineEmits<{
    update: [value: { key: keyof Character; value: unknown }]
}>();

function update(skills: BaseSkill[]) {
    emits('update', { key: 'savingThrows', value: skills });
}
</script>