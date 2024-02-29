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
                    <v-row v-for="ability in character.abilities.values()" v-bind:key="ability.type" align="center" no-gutters
                        class="pr-1">
                        <v-col>
                            <div class="text-body">
                                {{ ability.type }}: {{ ability.score }}
                            </div>
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
                                                Enter the new proficiency for
                                                {{ ability.type }}.
                                            </div>
                                            <v-text-field label="Score" v-model="dialogAbilityScore">
                                            </v-text-field>
                                        </v-card-text>

                                        <v-card-actions>
                                            <v-spacer></v-spacer>
                                            <v-btn text="Cancel" @click="isActive.value = false"></v-btn>
                                            <v-btn text="Confirm" @click="
                                                editAbility(
                                                    isActive,
                                                    ability,
                                                    dialogAbilityScore
                                                )
                                                "></v-btn>
                                        </v-card-actions>
                                    </v-card>
                                </template>
                            </v-dialog>
                        </v-col>
                    </v-row>
                </v-col>
                <v-divider vertical> </v-divider>
                <v-col cols="7">
                    <v-row v-for="skill in character.skills.values()" v-bind:key="skill.type" align="center" no-gutters
                        class="pl-3">
                        <v-col>
                            <div class="text-body">
                                {{ skill.type }}: {{ skill.proficiency }}
                            </div>
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
                                                Enter the new proficiency for
                                                {{ skill.type }}.
                                            </div>
                                            <v-combobox label="Proficiency" v-model="dialogSkillProficiency" :items="[
                                                Proficiency.Untrained,
                                                Proficiency.Trained,
                                                Proficiency.Expert,
                                                Proficiency.Master,
                                                Proficiency.Legendary,
                                            ]">
                                            </v-combobox>
                                        </v-card-text>

                                        <v-card-actions>
                                            <v-spacer></v-spacer>
                                            <v-btn text="Cancel" @click="isActive.value = false"></v-btn>
                                            <v-btn text="Confirm" @click="
                                                updateSkill(
                                                    isActive,
                                                    skill,
                                                    dialogSkillProficiency
                                                )
                                                "></v-btn>
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

import { onMounted, ref, type Ref } from 'vue';

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
    // TODO: If we decide to keep calculcations off client - also should update modifier
    if (char.id == notification.characterId) {
        if (char.abilities.get(notification.type) != undefined) {
            char.abilities.get(notification.type)!.score = notification.score;
        }
    }
}
function updateSkillFromNotification(notification: SkillEditedNotification) {
    const char = props.character;
    // TODO: If we decide to keep calculcations off client - also should update bonus
    if (char.id == notification.characterId) {
        if (char.skills.get(notification.type) != undefined) {
            char.skills.get(notification.type)!.proficiency = notification.proficiency;
        }
    }
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
