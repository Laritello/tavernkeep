<template>
    <div class="grid grid-cols-2 gap-x-2 gap-y-3">
        <AbilityView v-for="[key, ability] in userCharacter?.abilities" :key="key.toString()" :ability="ability" />
    </div>
</template>
<script setup lang="ts">
import { useAppStore } from '@/stores/app.store';

import AbilityView from './AbilityView.vue';
import { onMounted, ref } from 'vue';
import { Character } from '@/entities/Character';
const userCharacter = ref<Character>();

const appStore = useAppStore();
onMounted(() => {
    const currentUser = appStore.users.current;
    userCharacter.value = currentUser?.activeCharacter;
    console.log(currentUser);
});
</script>
