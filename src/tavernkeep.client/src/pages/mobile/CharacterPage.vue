<script setup lang="ts">
import SkillsWidget from '@/components/character/SkillsWidget.vue';
import AbilitiesWidget from '@/components/character/AbilitiesWidget.vue';
import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';
import SavingThrowsWidgetView from '@/components/character/SavingThrowsWidgetView.vue';
import { el } from 'vuetify/locale';
import { head } from 'node_modules/axios/index.cjs';
import { onMounted } from 'vue';

const user = useCurrentUserAccount();
const character = user.activeCharacter;

interface Section {
    link: string;
    header: string;
}

const sections: Section[] = [
    { link: '#attributes', header: 'Attributes' },
    { link: '#saving-throws', header: 'Saving Throws' },
    { link: '#skills', header: 'Skills' },
    { link: '#attacks', header: 'Attacks' },
    { link: '#spells', header: 'Spells' },
    { link: '#inventory', header: 'Inventory' },
]

onMounted(() => {
    updateSection();
});

function updateSection() {
    let headers = document.querySelectorAll('h2');
    let navigation = document.querySelectorAll('#sections-bar a');

    let sectionBar = document.querySelector('#sections-bar');

    /*
    * Find first header that hasn't reach the border
    */
    let border = sectionBar!.getBoundingClientRect().bottom;
    let start = headers.item(0).getBoundingClientRect().top;
    let sectionIndex = 0;

    if (border > start) {
        for (let i = 1; i < headers.length; i++) {
            let rect = headers.item(i).getBoundingClientRect();
            if (rect.top > border) {
                sectionIndex = i;
                break;
            }
        }

        /*
        *   If every single header is beyond border - assume the last section is selected
        */
        if (border > headers.item(headers.length - 1).getBoundingClientRect().top) {
            sectionIndex = headers.length - 1;
        }
    }

    /*
    * If header is not close enough to the border, we're still looking at previous section
    */
    sectionIndex = (sectionIndex == 0 || (headers.item(sectionIndex).getBoundingClientRect().top - border) <= 60) ? sectionIndex : sectionIndex - 1;
    let target = headers.item(sectionIndex).innerText;

    navigation.forEach((element, index) => {
        if (element.innerHTML == target) {
            element.classList.add("active");
        } else {
            element.classList.remove("active");
        }
    });
}
</script>

<template>
    <div v-if="character !== undefined" class="flex flex-col max-h-full max-h-full">
        <div class="flex flew-row flex-nowrap min-h-fit overflow-auto no-scrollbar gap-2 px-2" id="sections-bar">
            <a v-for="section in sections" :key="section.link" v-bind:href="section.link"
                class="p-1 pb-0 grow text-nowrap tracking-tight border-b-4 select-none">{{ section.header }}</a>
        </div>
        <div v-on:scroll="updateSection" class="flex flex-col overflow-y-auto p-2 gap-2 scroll-smooth no-scrollbar">
            <AbilitiesWidget id="attributes" :abilities="character.abilities" />
            <SavingThrowsWidgetView id="saving-throws" :savingThrows="character.savingThrows" />
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

.active {
    font-weight: bold;
}
</style>
