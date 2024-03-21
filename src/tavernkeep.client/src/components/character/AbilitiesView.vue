<template>
    <div class="flex flex-wrap justify-center gap-2">
        <AbilityView
            v-for="ability in abilities"
            :key="ability.type.toString()"
            :ability="ability"
            @click="() => showEditDialog(ability)"
        />
    </div>
</template>
<script setup lang="ts">
import type { Ability } from '@/contracts/character';

import AbilityView from './AbilityView.vue';
import AbilityEditDialog from '../dialogs/AbilityEditDialog.vue';
import { useModal } from '@/composables/useModal';
import type { AbilityType } from '@/contracts/enums';

const modal = useModal();
const { abilities } = defineProps<{ abilities: Record<AbilityType, Ability> }>();

async function showEditDialog(ability: Ability) {
    const result = await modal.showWithResult(AbilityEditDialog, {} as Ability, { ability });
    if (result.action === 'result') {
        ability.score = result.payload.score;
    }
}
</script>
