<template>
    <ProficiencyListEdit locale-prefix="pf.skills." v-model="skills" @updated="update"/>
</template>

<script setup lang="ts">
import type { Character } from '@/entities';
import ProficiencyListEdit from '../../shared/ProficiencyListEdit/ProficiencyListEdit.vue';
import type { BaseSkill } from '@/contracts/character';

const { character } = defineProps<{
    character: Character
}>();

const skills = [...character.skills];

const emits = defineEmits<{
    update: [value: { key: keyof Character; value: unknown }]
}>();

function update(skills: BaseSkill[]) {
    emits("update", { key: "skills", value: skills });
}
</script>