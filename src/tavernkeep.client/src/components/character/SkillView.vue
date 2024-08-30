<template>
    <tr>
        <th>{{ skill.proficiency.slice(0, 3) }}</th>
        <td>{{ skill.type }}</td>
        <td>{{ skill.bonus }}</td>
        <td class="py-1">
            <d20 @click="emit('roll', skill)" class="btn btn-ghost btn-circle size-8 fill-red-500" />
        </td>
    </tr>
</template>

<script lang="ts" setup>
import type { Skill } from '@/contracts/character';
import d20 from '@/assets/dice/d20-grey.svg';

const { skill } = defineProps<{
    skill: Skill;
}>();

const emit = defineEmits<{
    roll: [Skill];
}>();
</script>
<style></style>
