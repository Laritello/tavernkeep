<template>
    <v-card width="500">
        <v-card-title>{{ character.name }}</v-card-title>
        <v-container>
            <v-row no-gutters>
                <!--
                        Type error: looks like axios doesn't fully parses objects to respective types, but only keeps the structure.
                        Thus, methods for classes (getAbilities, getSkills) are not accessible. 
                    -->
                <v-col cols="5">
                    <v-row
                        v-for="ability in getAbilities(character)"
                        v-bind:key="ability.type"
                        align="center"
                        no-gutters
                        class="pr-1"
                    >
                        <v-col>
                            <div class="text-body">
                                {{ ability.type }}: {{ ability.score }}
                            </div>
                        </v-col>
                        <v-col cols="auto">
                            <v-dialog width="500">
                                <template v-slot:activator="{ props }">
                                    <v-btn
                                        v-bind="props"
                                        icon="$edit"
                                        size="x-small"
                                        variant="text"
                                    ></v-btn>
                                </template>

                                <template v-slot:default="{ isActive }">
                                    <v-card title="Edit skill">
                                        <v-card-text>
                                            <div class="mb-1">
                                                Enter the new proficiency for
                                                {{ ability.type }}.
                                            </div>
                                            <v-text-field
                                                label="Score"
                                                v-model="dialogAbilityScore"
                                            >
                                            </v-text-field>
                                        </v-card-text>

                                        <v-card-actions>
                                            <v-spacer></v-spacer>
                                            <v-btn
                                                text="Cancel"
                                                @click="isActive.value = false"
                                            ></v-btn>
                                            <v-btn
                                                text="Confirm"
                                                @click="
                                                    editAbility(
                                                        isActive,
                                                        ability,
                                                        dialogAbilityScore
                                                    )
                                                "
                                            ></v-btn>
                                        </v-card-actions>
                                    </v-card>
                                </template>
                            </v-dialog>
                        </v-col>
                    </v-row>
                </v-col>
                <v-divider vertical> </v-divider>
                <v-col cols="7">
                    <v-row
                        v-for="skill in getSkills(character)"
                        v-bind:key="skill.type"
                        align="center"
                        no-gutters
                        class="pl-3"
                    >
                        <v-col>
                            <div class="text-body">
                                {{ skill.type }}: {{ skill.proficiency }}
                            </div>
                        </v-col>
                        <v-col cols="auto">
                            <v-dialog width="500">
                                <template v-slot:activator="{ props }">
                                    <v-btn
                                        v-bind="props"
                                        icon="$edit"
                                        size="x-small"
                                        variant="text"
                                    ></v-btn>
                                </template>

                                <template v-slot:default="{ isActive }">
                                    <v-card title="Edit skill">
                                        <v-card-text>
                                            <div class="mb-1">
                                                Enter the new proficiency for
                                                {{ skill.type }}.
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
                                            <v-btn
                                                text="Cancel"
                                                @click="isActive.value = false"
                                            ></v-btn>
                                            <v-btn
                                                text="Confirm"
                                                @click="
                                                    updateSkill(
                                                        isActive,
                                                        skill,
                                                        dialogSkillProficiency
                                                    )
                                                "
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
    </v-card>
</template>

<script setup lang="ts">
import type { Ability } from '@/contracts/character/Ability';
import type { Skill } from '@/contracts/character/Skill';
import { type Character } from '@/entities/Character';
import { Proficiency } from '@/contracts/enums/Proficiency';
import type { ApiClient } from '@/api/base/ApiClient';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import CharacterHub from '@/api/hubs/CharacterHub';
import type { AbilityEditedNotification } from '@/contracts/notifications/AbilityEditedNotification';
import type { SkillEditedNotification } from '@/contracts/notifications/SkillEditedNotification';
import { AbilityType } from '@/contracts/enums/AbilityType';
import { SkillType } from '@/contracts/enums/SkillType';

import { defineProps, onMounted, ref, type Ref } from 'vue';

const client: ApiClient = ApiClientFactory.createApiClient();

const props = defineProps<{
    character: Character;
}>();

const dialogAbilityScore = ref(0);
const dialogSkillProficiency = ref(Proficiency.Untrained);

onMounted(() => {
    CharacterHub.connection.on(
        'OnAbilityEdited',
        (notification: AbilityEditedNotification) => {
            updateAbilityFromNotification(notification);
        }
    );

    CharacterHub.connection.on(
        'OnSkillEdited',
        (notification: SkillEditedNotification) => {
            updateSkillFromNotification(notification);
        }
    );
});

function updateAbilityFromNotification(
    notification: AbilityEditedNotification
) {
    const char = props.character;

    // Move to class, use filter in array instead of switch
    if (char.id == notification.characterId) {
        switch (notification.type) {
            case AbilityType.Strength:
                char.strength.score = notification.score;
                break;
            case AbilityType.Dexterity:
                char.dexterity.score = notification.score;
                break;
            case AbilityType.Constitution:
                char.constitution.score = notification.score;
                break;
            case AbilityType.Intelligence:
                char.intelligence.score = notification.score;
                break;
            case AbilityType.Wisdom:
                char.wisdom.score = notification.score;
                break;
            case AbilityType.Charisma:
                char.charisma.score = notification.score;
                break;
        }
    }
}
function updateSkillFromNotification(notification: SkillEditedNotification) {
    const char = props.character;

    // Move to class, use filter in array instead of switch
    if (char.id == notification.characterId) {
        switch (notification.type) {
            case SkillType.Acrobatics:
                char.acrobatics.proficiency = notification.proficiency;
                break;
            case SkillType.Arcana:
                char.arcana.proficiency = notification.proficiency;
                break;
            case SkillType.Athletics:
                char.athletics.proficiency = notification.proficiency;
                break;
            case SkillType.Crafting:
                char.crafting.proficiency = notification.proficiency;
                break;
            case SkillType.Deception:
                char.deception.proficiency = notification.proficiency;
                break;
            case SkillType.Diplomacy:
                char.diplomacy.proficiency = notification.proficiency;
                break;
            case SkillType.Intimidation:
                char.intimidation.proficiency = notification.proficiency;
                break;
            case SkillType.Medicine:
                char.medicine.proficiency = notification.proficiency;
                break;
            case SkillType.Nature:
                char.nature.proficiency = notification.proficiency;
                break;
            case SkillType.Occultism:
                char.occultism.proficiency = notification.proficiency;
                break;
            case SkillType.Performance:
                char.performance.proficiency = notification.proficiency;
                break;
            case SkillType.Religion:
                char.religion.proficiency = notification.proficiency;
                break;
            case SkillType.Society:
                char.society.proficiency = notification.proficiency;
                break;
            case SkillType.Stealth:
                char.stealth.proficiency = notification.proficiency;
                break;
            case SkillType.Survival:
                char.survival.proficiency = notification.proficiency;
                break;
            case SkillType.Thievery:
                char.thievery.proficiency = notification.proficiency;
                break;
        }
    }
}
function getAbilities(character: Character): Ability[] {
    return [
        character.strength,
        character.dexterity,
        character.constitution,
        character.intelligence,
        character.wisdom,
        character.charisma,
    ];
}
function getSkills(character: Character): Skill[] {
    return [
        character.acrobatics,
        character.arcana,
        character.athletics,
        character.crafting,
        character.deception,
        character.diplomacy,
        character.intimidation,
        character.medicine,
        character.nature,
        character.occultism,
        character.performance,
        character.religion,
        character.society,
        character.stealth,
        character.survival,
        character.thievery,
    ];
}
async function editAbility(
    isActive: Ref<boolean>,
    ability: Ability,
    score: number
) {
    var response = await client.editAbility(
        props.character.id,
        ability.type,
        score
    );

    ability.score = response.data.score;
    dialogAbilityScore.value = 0;
    isActive.value = false;
}
async function updateSkill(
    isActive: Ref<boolean>,
    skill: Skill,
    proficiency: Proficiency
) {
    var response = await client.editSkill(
        props.character.id,
        skill.type,
        proficiency
    );

    skill.proficiency = response.data.proficiency;
    dialogSkillProficiency.value = Proficiency.Untrained; // TODO: On show set selected skill current value
    isActive.value = false;
}
</script>
