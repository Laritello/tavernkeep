<template>
    <v-sheet class="mx-auto pa-2">
        <v-card v-for="user in users.filter(u => u.activeCharacter != undefined)" :key="user.id" width="500">
            <v-card-title>{{ user.activeCharacter?.name }}</v-card-title>
            <v-container>
                <v-row no-gutters>
                    <!--
                        Type error: looks like axios doesn't fully parses objects to respective types, but only keeps the structure.
                        Thus, methods for classes (getAbilities, getSkills) are not accessible. 
                    -->
                    <v-col cols="5">
                        <v-row v-for="ability in getAbilities(user.activeCharacter)" v-bind:key="ability.type"
                            align="center" no-gutters class="pr-1">
                            <v-col>
                                <div class="text-body">{{ ability.type }}: {{ ability.score }}</div>
                            </v-col>
                            <v-col cols="auto">
                                <v-btn icon="$edit" size="x-small" variant="text"></v-btn>
                            </v-col>
                        </v-row>
                    </v-col>
                    <v-divider vertical>

                    </v-divider>
                    <v-col cols="7">
                        <v-row v-for="skill in getSkills(user.activeCharacter)" v-bind:key="skill.type" align="center"
                            no-gutters class="pl-3">
                            <v-col>
                                <div class="text-body">{{ skill.type }}: {{ skill.proficiency }}</div>
                            </v-col>
                            <v-col cols="auto">
                                <v-dialog width="500">
                                    <template v-slot:activator="{ props }">
                                        <v-btn v-bind="props" icon="$edit" size="x-small" variant="text"></v-btn>
                                    </template>

                                    <template v-slot:default="{ isActive }">
                                        <v-card title="Edit skill">
                                            <v-card-text>
                                                Enter the new proficiency for {{ skill.type }}.
                                                <v-combobox label="{{ skill.type }}" :v-model="dialogSkillProficiency"
                                                    :items="[Proficiency.Untrained, Proficiency.Trained, Proficiency.Expert, Proficiency.Master, Proficiency.Legendary]">
                                                </v-combobox>
                                            </v-card-text>

                                            <v-card-actions>
                                                <v-spacer></v-spacer>
                                                <v-btn text="Cancel" @click="isActive.value = false"></v-btn>
                                                <v-btn text="Confirm" @click="updateSkill(isActive, skill, dialogSkillProficiency)"></v-btn>
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
    </v-sheet>
</template>

<script lang="ts">
import type { Ability } from '@/contracts/character/Ability';
import type { Skill } from '@/contracts/character/Skill';
import type { Character } from '@/entities/Character';
import type { User } from '@/entities/User';
import { defineComponent, type PropType, type Ref } from 'vue'
import { Proficiency } from '@/contracts/enums/Proficiency'

interface CharacterComponentData {
    dialogAbilityScore: number
    dialogSkillProficiency: Proficiency,
}

export default defineComponent({
    props: {
        users: {
            type: Object as PropType<User[]>,
            required: true
        }
    },
    data(): CharacterComponentData {
        return {
            dialogAbilityScore: 0,
            dialogSkillProficiency: Proficiency.Untrained
        };
    },
    setup() {
        return { Proficiency }
    },
    methods: {
        getAbilities(character: Character): Ability[] {
            return [character.strength, character.dexterity, character.constitution, character.intelligence, character.wisdom, character.charisma]
        },
        getSkills(character: Character): Skill[] {
            return [
                character.acrobatics, character.arcana, character.athletics, character.crafting, character.deception, character.diplomacy, character.intimidation,
                character.medicine, character.nature, character.occultism, character.performance, character.religion, character.society, character.stealth,
                character.survival, character.thievery
            ]
        },
        updateAbility(isActive: Ref<boolean>, ability: Ability, score: number) {
            // TODO: Move skill/abilities into separate components
            ability.score = score
            console.log('Ability ' + ability.type + " value set to " + ability.score)
        },
        updateSkill(isActive: Ref<boolean>, skill: Skill, proficiency: Proficiency) {
            skill.proficiency = proficiency
            console.log('Ability ' + skill.type + " value set to " + skill.proficiency)
        }
    }
})
</script>