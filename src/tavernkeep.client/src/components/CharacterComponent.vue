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

async function updateSkill(isActive: Ref<boolean>, skill: Skill, proficiency: Proficiency) {
    var response = await client.editSkill(character.id, skill.type, proficiency);

    skill.proficiency = response.proficiency;
    dialogSkillProficiency.value = Proficiency.Untrained; // TODO: On show set selected skill current value
    isActive.value = false;
}

async function updateLore(isActive: Ref<boolean>, lore: Lore, proficiency: Proficiency) {
    var response = await client.editLore(character.id, lore.topic, proficiency);

    lore.proficiency = response.proficiency;
    dialogSkillProficiency.value = Proficiency.Untrained; // TODO: On show set selected lore skill current value
    isActive.value = false;
}
</script>
<template>
    <v-card width="500">
        <v-card-title>{{ character.name }}</v-card-title>
        <v-container>
            <v-row no-gutters>
                <v-divider vertical> </v-divider>
                <v-col cols="7">
                    <v-row>
                        <AbilitiesView :abilities="character.abilities" @edit="showEditAbilityDialog" />
                    </v-row>
                    <v-row>
                        <SkillsView :skills="character.skills" />
                    </v-row>
                    <v-row
                        v-for="value in character.lores"
                        v-bind:key="value.topic"
                        align="center"
                        no-gutters
                        class="pl-3"
                    >
                        <v-col>
                            <div class="text-body">Lore ({{ value.topic }}): {{ value.proficiency }}</div>
                        </v-col>
                        <v-col cols="auto">
                            <v-dialog width="500">
                                <template v-slot:activator="{ props }">
                                    <v-btn v-bind="props" icon="$edit" size="x-small" variant="text"></v-btn>
                                </template>

                                <template v-slot:default="{ isActive }">
                                    <v-card title="Edit skill">
                                        <v-card-text>
                                            <div class="mb-1">
                                                Enter the new proficiency for Lore({{ value.topic }}).
                                            </div>
                                            <v-combobox
                                                label="Proficiency"
                                                v-model="dialogSkillProficiency"
                                                :items="[
                                                    Proficiency.Untrained,
                                                    Proficiency.Trained,
                                                    Proficiency.Expert,
                                                    Proficiency.Master,
                                                    Proficiency.Legendary,
                                                ]"
                                            >
                                            </v-combobox>
                                        </v-card-text>

                                        <v-card-actions>
                                            <v-spacer></v-spacer>
                                            <v-btn text="Cancel" @click="isActive.value = false"></v-btn>
                                            <v-btn
                                                text="Confirm"
                                                @click="updateLore(isActive, value, dialogSkillProficiency)"
                                            ></v-btn>
                                        </v-card-actions>
                                    </v-card>
                                </template>
                            </v-dialog>
                        </v-col>
                    </v-row>
                </v-col>
            </v-row>
        </v-container>
        <v-container>
            <ConditionsView :characterId="character.id" :conditions="character.conditions" />
        </v-container>
    </v-card>
</template>
