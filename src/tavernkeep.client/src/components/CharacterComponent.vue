<script setup lang="ts">
import type { Ability } from '@/contracts/character/Ability';
import type { Skill } from '@/contracts/character/Skill';
import { type Character } from '@/entities/Character';
import { Proficiency } from '@/contracts/enums/Proficiency';
import { ApiClientFactory } from '@/factories/ApiClientFactory';

import { ref, type Ref } from 'vue';
import type { Lore } from '@/contracts/character/Lore';
import ConditionsView from '@/components/character/ConditionsView.vue';
import AbilitiesView from './character/AbilitiesView.vue';
import SkillsView from './character/SkillsView.vue';
import AbilityEditDialog from '@/components/dialogs/AbilityEditDialog.vue';
import { useModal } from '@/composables/useModal';
import SkillEditDialog from './dialogs/SkillEditDialog.vue';

const client = ApiClientFactory.createApiClient();

const { character } = defineProps<{
    character: Character;
}>();

const dialogSkillProficiency = ref(Proficiency.Untrained);

async function showEditAbilityDialog(ability: Ability) {
    const modal = useModal();
    const result = await modal.show(AbilityEditDialog, { ability });
    if (result.action === 'result') {
        ability.score = result.payload.score;
        client.editAbility(character.id, ability.type, ability.score);
    }
}

async function showEditSkillDialog(skill: Skill) {
    const modal = useModal();
    const result = await modal.show(SkillEditDialog, { skill });
    if (result.action === 'result') {
        skill.proficiency = result.payload.proficiency;
        await client.editSkill(character.id, skill.type, skill.proficiency);
    }
}

async function updateLore(isActive: Ref<boolean>, lore: Lore, proficiency: Proficiency) {
    var response = await client.editLore(character.id, lore.topic, proficiency);

    lore.proficiency = response.proficiency;
    dialogSkillProficiency.value = Proficiency.Untrained; // TODO: On show set selected lore skill current value
    isActive.value = false;
}
</script>
<template>
    <div class="p-3">
        <h1 class="text-4xl py-2 font-semibold">{{ character.name }}</h1>
        <div class="container flex gap-2">
            <div class="flex flex-col gap-2">
                <AbilitiesView :abilities="character.abilities" @edit="showEditAbilityDialog" />
                <SkillsView :skills="character.skills" @edit="showEditSkillDialog" />
                <v-row v-for="value in character.lores" v-bind:key="value.topic" align="center" no-gutters class="pl-3">
                    <v-col>
                        <div class="text-body">Lore ({{ value.topic }}): {{ value.proficiency }}</div>
                    </v-col>
                </v-row>
            </div>
            <ConditionsView :characterId="character.id" :conditions="character.conditions" class="h-fit" />
        </div>
    </div>
</template>
