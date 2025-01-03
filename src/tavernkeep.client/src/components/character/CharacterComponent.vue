<script setup lang="ts">
import type { Ability, Perception, Skill, SavingThrow } from '@/contracts/character';
import type { Character } from '@/entities/Character';

import { ApiClientFactory } from '@/factories/ApiClientFactory';
import { useModal } from '@/composables/useModal';

import CharacterHeaderView from '@/components/character/CharacterHeaderView.vue';
import ConditionsView from '@/components/character/ConditionsView.vue';
import AbilitiesWidget from '@/components/character/AbilitiesWidget.vue';
import SkillsWidget from '@/components/character/SkillsWidget.vue';
import SavingThrowsView from './SavingThrowsView.vue';
import ArmorClassWidget from './ArmorClassWidget.vue';
import PerceptionWidget from './PerceptionWidget.vue';

import ProficiencyEditDialog from '@/components/dialogs/ProficiencyEditDialog.vue';
import AbilityEditDialog from '@/components/dialogs/AbilityEditDialog.vue';
import ConditionApplyDialog from '../dialogs/ConditionApplyDialog.vue';
import type { Condition } from '@/entities';


const client = ApiClientFactory.createApiClient();
const modal = useModal();

const { character } = defineProps<{
    character: Character;
}>();

async function showEditAbilityDialog(ability: Ability) {
    const result = await modal.show(AbilityEditDialog, { ability });
    if (result.action === 'result') {
        client.editAbility(character.id, ability.type, result.payload.score);
    }
}

async function showEditSkillDialog(skill: Skill) {
    const result = await modal.show(ProficiencyEditDialog, { proficiency: skill.proficiency });
    if (result.action === 'result') {
        await client.editSkills(character.id, skill.type, result.payload.value);
    }
}

async function showSavingThrowEditDialog(savingThrow: SavingThrow) {
    const result = await modal.show(ProficiencyEditDialog, { proficiency: savingThrow.proficiency });
    if (result.action === 'result') {
        await client.editSavingThrows(character.id, savingThrow.type, result.payload.value);
    }
}

async function showPerceptionEditDialog(perception: Perception) {
    const result = await modal.show(ProficiencyEditDialog, { proficiency: perception.proficiency });
    if (result.action === 'result') {
        await client.editPerception(character.id, result.payload.value);
    }
}

async function showConditionApplyDialog() {
    const result = await modal.show(ConditionApplyDialog, { conditions: await client.getConditions() });
    if (result.action === 'result') {
        await client.applyCondition(character.id, result.payload.name, result.payload.level);
    }
}

async function removeCondition(condition: Condition) {
    await client.removeCondition(character.id, condition.name);
}

async function increaseCondtionLevel(condition: Condition) {
    await client.applyCondition(character.id, condition.name, condition.level + 1);
}

async function decreaseCondtionLevel(condition: Condition) {
    await client.applyCondition(character.id, condition.name, condition.level - 1);
}
</script>
<template>
    <div class="p-3">
        <CharacterHeaderView :name="character.name" :health="character.health" class="mb-2" />
        <div class="container flex gap-2">
            <div class="flex flex-col gap-2">
                <AbilitiesWidget :abilities="character.abilities" @edit="showEditAbilityDialog" />
                <SkillsWidget :skills="character.skills" @edit="showEditSkillDialog" />
            </div>
            <div class="flex flex-col gap-2 h-fit">
                <PerceptionWidget :perception="character.perception" @edit="showPerceptionEditDialog" />
                <ArmorClassWidget :armor="character.armor" />
                <SavingThrowsView :saving-throws="character.savingThrows" @edit="showSavingThrowEditDialog" />
                <ConditionsView
                    :conditions="character.conditions"
                    @addCondition="showConditionApplyDialog"
                    @removeCondition="removeCondition"
                    @increaseConditionLevel="increaseCondtionLevel"
                    @decreaseConditionLevel="decreaseCondtionLevel"
                />
            </div>
        </div>
    </div>
</template>
