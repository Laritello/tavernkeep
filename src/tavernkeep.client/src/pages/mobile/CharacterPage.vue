<script setup lang="ts">
import { onMounted } from 'vue';
import { useToast } from "vue-toastification";
import { useI18n } from 'vue-i18n';
import AbilitiesWidget from '@/components/character/widgets/Abilities/AbilitiesWidget.vue';
import SavingThrowsWidget from '@/components/character/widgets/SavingThrows/SavingThrowsWidget.vue';
import SkillsWidget from '@/components/character/widgets/Skills/SkillsWidget.vue';
import SkillCheckResultToast from '@/components/toasts/SkillCheckResultToast.vue';
import SavingThrowResultToast from '@/components/toasts/SavingThrowResultToast.vue';
import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';
import { AbilityType, RollType, SavingThrowType, type Proficiency, type SkillType } from '@/contracts/enums';
import type { AxiosApiClient } from '@/api/axios/AxiosApiClient';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import ArmorWidget from '@/components/character/widgets/Armor/ArmorWidget.vue';
import type { Armor } from '@/contracts/character';
import SpeedsWidget from '@/components/character/widgets/Speeds/SpeedsWidget.vue';

const { t } = useI18n();
const api: AxiosApiClient = ApiClientFactory.createApiClient();

const user = useCurrentUserAccount();

const character = user.activeCharacter;

interface Section {
    link: string;
    header: string;
}

const sections: Section[] = [
    { link: '#attributes', header: t('sections.attributes') },
    { link: '#saving-throws', header: t('sections.savingThrows') },
    { link: '#skills', header: t('sections.skills') },
    { link: '#armor', header: t('sections.armor') },
    { link: '#attacks', header: t('sections.attacks')},
    { link: '#spells', header: t('sections.spells') },
    { link: '#inventory', header: t('sections.inventory') },
]

async function updateAbilities(scores: Record<AbilityType, number>) {
    if (character.value !== undefined) {
        await api.editAbilities(character.value.id, scores);
    }
}
async function updateSkills(proficiencies: Record<SkillType, Proficiency>) {
    if (character.value !== undefined) {
        await api.editSkills(character.value.id, proficiencies);
    }
}

async function updateSavingThrows(proficiencies: Record<string, Proficiency>) {
    if (character.value !== undefined) {
        await api.editSavingThrows(character.value.id, proficiencies);
    }
}

async function updateArmor(armor: Armor) {
    if (character.value !== undefined) {
        let equipped = armor.equipped;
        await api.editArmor(character.value.id, equipped.type, equipped.bonus, equipped.hasDexterityCap, equipped.dexterityCap, armor.proficiencies);
    }
}

// TODO: Longtap for private roll or toggle somewhere for the roll type
async function rollSkillCheck(skillType: SkillType) {
    if (character.value !== undefined) {
        const message = await api.performSkillCheck(character.value.id, skillType, RollType.Public);
        const toast = useToast();
        toast({
            component: SkillCheckResultToast,
            props: {
                message
            }
        }, {
            toastClassName: 'skill-check-toast',
        });
    }
}

async function rollSavingThrow(savingThrow: SavingThrowType) {
    if (character.value !== undefined) {
        const message = await api.performSavingThrow(character.value.id, savingThrow, RollType.Public);
        const toast = useToast();
        toast({
            component: SavingThrowResultToast,
            props: {
                message
            }
        }, {
            toastClassName: 'saving-throw-toast',
        });
    }
}



onMounted(() => {
    updateSection();
});

// TODO: scroll navigation bar to the selected tab
/*
* Update section based on scrolled content.
*/
function updateSection() {
    // Collect all required elements
    // TODO: use vue refs instead
    let headers = document.querySelectorAll('h2');
    let navigation = document.querySelectorAll('#sections-bar a');

    let sectionBar = document.querySelector('#sections-bar');

    if (sectionBar == undefined) {
        return;
    }

    // Find first header that hasn't reached the border
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

        // If every single header is beyond border - assume the last section is selected
        if (border > headers.item(headers.length - 1).getBoundingClientRect().top) {
            sectionIndex = headers.length - 1;
        }
    }

    // If header is not close enough to the border, we're still looking at previous section
    sectionIndex = (sectionIndex == 0 || (headers.item(sectionIndex).getBoundingClientRect().top - border) <= 60) ? sectionIndex : sectionIndex - 1;
    let target = headers.item(sectionIndex).innerText;

    // Update styleclass for each navigation button
    navigation.forEach((element) => {
        if (element.innerHTML == target) {
            element.classList.add("text-primary");
            element.classList.add("active");
        } else {
            element.classList.remove("text-primary");
            element.classList.remove("active");
        }
    });
}
</script>

<template>
    <div v-if="character !== undefined" class="flex flex-col max-h-full">
        <!--Header-->
        <div class="sticky bg-base-100 flex flew-row flex-nowrap min-h-fit overflow-auto no-scrollbar lg:hidden"
            id="sections-bar">
            <a v-for="section in sections" :key="section.link" v-bind:href="section.link"
                class="py-1 px-4 pb-0 grow text-nowrap tracking-tight border-b-2 border-base-300 select-none">{{
                    section.header }}</a>
        </div>

        <!--Character Sheet Content-->
        <div v-on:scroll="updateSection"
            class="flex flex-col overflow-y-auto p-2 gap-2 scroll-smooth no-scrollbar bg-base-200">
            <AbilitiesWidget id="attributes" :abilities="character.abilities" @changed="updateAbilities" />
            <SavingThrowsWidget id="saving-throws" :savingThrows="character.savingThrows" @changed="updateSavingThrows"
                @roll="(type) => rollSavingThrow(type)" />
            <SkillsWidget id="skills" :skills="character.skills" @changed="updateSkills"
                @roll="(type) => rollSkillCheck(type)" />
            <ArmorWidget id="armor" :armor="character.armor" @changed="updateArmor"/>
            <SpeedsWidget id="speeds" :speeds="character.speeds" />
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
    border-color: currentColor;
}
</style>
