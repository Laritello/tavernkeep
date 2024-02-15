<template>
    <v-sheet class="mx-auto pa-2">
        <v-card v-for="user in users.filter(u => u.activeCharacter != undefined)" :key="user.id" width="350">
            <v-card-title>{{ user.activeCharacter?.name }}</v-card-title>
            <v-container>
                <v-row no-gutters>
                    <v-col cols="6">
                        <v-row v-for="ability in user.activeCharacter?.getAllAbilities()" :key="ability.type">
                            <v-col>
                                <div class="text-body">{{ ability.type }}: {{ ability.score }}</div>
                            </v-col>
                        </v-row>
                    </v-col>
                    <v-col cols="6">
                        <v-row v-for="skill in user.activeCharacter?.getSkills()" :key="skill.type">
                            <v-col>
                                <div class="text-body">{{ skill.type }}: {{ skill.proficiency }}</div>
                            </v-col>
                        </v-row>
                    </v-col>
                </v-row>
            </v-container>
        </v-card>
    </v-sheet>
</template>

<script lang="ts">
import type { User } from '@/entities/User';
import { defineComponent, type PropType } from 'vue'

export default defineComponent({
    props: {
        users: {
            type: Object as PropType<User[]>,
            required: true
        }
    },
})
</script>