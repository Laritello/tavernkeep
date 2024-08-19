<template>
    <select
        v-model="selected"
        class="select select-bordered border w-full max-w-xs"
        placeholder="Select proficiency level"
    >
        <option disabled selected :value="undefined">Select proficiency level</option>
        <option v-for="proficiency in proficiencies" :key="proficiency.name" :value="proficiency.value">
            {{ proficiency.name }}
        </option>
    </select>
</template>

<script setup lang="ts">
import { Proficiency } from '@/contracts/enums';
const proficiencies = [
    { name: 'Untrained', value: Proficiency.Untrained },
    { name: 'Trained', value: Proficiency.Trained },
    { name: 'Expert', value: Proficiency.Expert },
    { name: 'Master', value: Proficiency.Master },
    { name: 'Legendary', value: Proficiency.Legendary },
];

const selected = defineModel<Proficiency>();
</script>
