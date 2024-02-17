<template>
    <v-sheet class="mx-auto pa-2">
        <!-- <CharacterComponent
            v-for="character in roomUsersStore.users
                .filter((u) => u.activeCharacter != undefined)
                .map((u) => u.activeCharacter)"
            :key="character.id"
            :character="character"
        >
        </CharacterComponent> -->
    </v-sheet>
</template>

<script setup lang="ts">
import type { User } from '@/entities/User';
import CharacterComponent from './CharacterComponent.vue';
import { useUsersStore } from '@/stores/users.store';

const usersStore = useUsersStore();
</script>
@/stores/users.store