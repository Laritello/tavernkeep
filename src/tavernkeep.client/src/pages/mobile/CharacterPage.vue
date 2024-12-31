<script setup lang="ts">
import SkillsWidget from '@/components/character/SkillsWidget.vue';
import AbilitiesWidget from '@/components/character/AbilitiesWidget.vue';
import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';
import SavingThrowsWidgetView from '@/components/character/SavingThrowsWidgetView.vue';

const user = useCurrentUserAccount();
const character = user.activeCharacter;

interface Section {
    link: string;
    header: string;
}

const sections: Section[] = [
    { link: '#attributes', header: 'Attributes' },
    { link: '#saving-throws', header: 'Saving Throws' },
    { link: '#senses', header: 'Senses' },
    { link: '#skills', header: 'Skills' },
    { link: '#attacks', header: 'Attacks' },
    { link: '#spells', header: 'Spells' },
    { link: '#inventory', header: 'Inventory' },
]
</script>

<template>
    <div v-if="character !== undefined" class="flex flex-col max-h-full max-h-full">
        <div class="flex flew-row flex-nowrap min-h-fit overflow-auto no-scrollbar gap-2 px-2">
            <a v-for="section in sections" :key="section.link" v-bind:href="section.link"
            class="p-1 pb-0 grow text-nowrap tracking-tight border-b-4 select-none">{{ section.header }}</a>
        </div>
        <div class="flex flex-col overflow-y-auto p-2 gap-2 scroll-smooth">
            <AbilitiesWidget id="attributes" :abilities="character.abilities" />
            <SavingThrowsWidgetView id="saving-throws" :savingThrows="character.savingThrows"/>
            <SkillsWidget id="skills" :skills="character.skills" />
        </div>
    </div>
    <div v-else>No selected character</div>
</template>

<style scoped>
/* Hide scrollbar for Chrome, Safari and Opera */
.no-scrollbar::-webkit-scrollbar {
    display: none;
}

/* Hide scrollbar for IE, Edge and Firefox */
.no-scrollbar {
    /* IE and Edge */
    -ms-overflow-style: none;


    /* Firefox */
    scrollbar-width: none;
}
</style>
