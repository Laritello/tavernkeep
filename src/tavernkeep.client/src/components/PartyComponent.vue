<template>
    <v-sheet class="mx-auto pa-2">
        <CharacterComponent v-for="character in users.filter(u => u.activeCharacter != undefined).map(u => u.activeCharacter)" :key="character.id" :character="character">   
        </CharacterComponent>
    </v-sheet>
</template>

<script lang="ts">
import type { User } from '@/entities/User';
import { defineComponent, type PropType } from 'vue'
import CharacterComponent from './CharacterComponent.vue';

export default defineComponent({
    props: {
        users: {
            type: Object as PropType<User[]>,
            required: true
        }
    },
    components: { CharacterComponent }
})
</script>