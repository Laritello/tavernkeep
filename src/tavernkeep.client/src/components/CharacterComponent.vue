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
                        v-for="[key, value] in Object.entries(character.abilities)"
                        v-bind:key="key"
                        align="center"
                        no-gutters
                        class="pr-1"
                    >
                        <v-col>
                            <div class="text-body">{{ value.type }}: {{ value.score }}</div>
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
                                                {{ value.type }}.
                                            </div>
                                            <v-text-field label="Score" v-model="dialogAbilityScore"> </v-text-field>
                                        </v-card-text>

                                        <v-card-actions>
                                            <v-spacer></v-spacer>
                                            <v-btn text="Cancel" @click="isActive.value = false"></v-btn>
                                            <v-btn
                                                text="Confirm"
                                                @click="editAbility(isActive, value, dialogAbilityScore)"
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
                        v-for="[key, value] in Object.entries(character.skills)"
                        v-bind:key="key"
                        align="center"
                        no-gutters
                        class="pl-3"
                    >
                        <v-col>
                            <div class="text-body">{{ value.type }}: {{ value.proficiency }}</div>
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
                                                {{ value.type }}.
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
                                                @click="updateSkill(isActive, value, dialogSkillProficiency)"
                                            ></v-btn>
                                        </v-card-actions>
                                    </v-card>
                                </template>
                            </v-dialog>
                        </v-col>
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
    </v-card>
</template>

<script setup lang="ts">
import type { Ability } from '@/contracts/character/Ability';
import type { Skill } from '@/contracts/character/Skill';
import { type Character } from '@/entities/Character';
import { Proficiency } from '@/contracts/enums/Proficiency';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import CharacterHub from '@/api/hubs/CharacterHub';

import { onMounted, ref, type Ref } from 'vue';
import type { Lore } from '@/contracts/character/Lore';
import type { CharacterEditedNotification } from '@/contracts/notifications/CharacterEditedNotification';

const client = ApiClientFactory.createApiClient();

const props = defineProps<{
    character: Character;
}>();

const dialogAbilityScore = ref(0);
const dialogSkillProficiency = ref(Proficiency.Untrained);

onMounted(() => {
    CharacterHub.connection.on('OnCharacterEdited', (notification: CharacterEditedNotification) => {
        console.log(notification.character);
    });
});

async function editAbility(isActive: Ref<boolean>, ability: Ability, score: number) {
    var response = await client.editAbility(props.character.id, ability.type, score);

    ability.score = response.score;
    dialogAbilityScore.value = 0;
    isActive.value = false;
}

async function updateSkill(isActive: Ref<boolean>, skill: Skill, proficiency: Proficiency) {
    var response = await client.editSkill(props.character.id, skill.type, proficiency);

    skill.proficiency = response.proficiency;
    dialogSkillProficiency.value = Proficiency.Untrained; // TODO: On show set selected skill current value
    isActive.value = false;
}

async function updateLore(isActive: Ref<boolean>, lore: Lore, proficiency: Proficiency) {
    var response = await client.editLore(props.character.id, lore.topic, proficiency);

    lore.proficiency = response.proficiency;
    dialogSkillProficiency.value = Proficiency.Untrained; // TODO: On show set selected lore skill current value
    isActive.value = false;
}
</script>
