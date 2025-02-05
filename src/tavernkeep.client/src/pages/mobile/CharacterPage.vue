<script setup lang="ts">
import { onMounted } from 'vue';
import { useI18n } from 'vue-i18n';
import { useToast } from 'vue-toastification';

import type { AxiosApiClient } from '@/api/axios/AxiosApiClient';
import AbilitiesWidget from '@/components/character/widgets/Abilities/AbilitiesWidget.vue';
import ArmorWidget from '@/components/character/widgets/Armor/ArmorWidget.vue';
import SavingThrowsWidget from '@/components/character/widgets/SavingThrows/SavingThrowsWidget.vue';
import SkillsWidget from '@/components/character/widgets/Skills/SkillsWidget.vue';
import SpeedsWidget from '@/components/character/widgets/Speeds/SpeedsWidget.vue';
import SavingThrowResultToast from '@/components/toasts/SavingThrowResultToast.vue';
import SkillCheckResultToast from '@/components/toasts/SkillCheckResultToast.vue';
import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';
import type { Armor, Skill } from '@/contracts/character';
import type { SkillEditDto, SpeedEditDto } from '@/contracts/dtos';
import { RollType, SpeedType, type Proficiency } from '@/contracts/enums';
import { ApiClientFactory } from '@/factories/ApiClientFactory';

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
    { link: '#speeds', header: t('sections.speeds') },
    { link: '#attacks', header: t('sections.attacks') },
    { link: '#spells', header: t('sections.spells') },
    { link: '#inventory', header: t('sections.inventory') },
];

async function updateAbilities(scores: Record<string, number>) {
    if (character.value !== undefined) {
        await api.editAbilities(character.value.id, scores);
    }
}
async function updateSkills(skills: Record<string, SkillEditDto>) {
    if (character.value !== undefined) {
        await api.editSkills(character.value.id, skills);
    }
}

async function updateSavingThrows(proficiencies: Record<string, Proficiency>) {
    if (character.value !== undefined) {
        await api.editSavingThrows(character.value.id, proficiencies);
    }
}

async function updateSpeeds(speeds: Record<SpeedType, SpeedEditDto>) {
    if (character.value !== undefined) {
        await api.editSpeeds(character.value.id, speeds);
    }
}

async function updateArmor(armor: Armor) {
    if (character.value !== undefined) {
        const equipped = armor.equipped;
        await api.editArmor(
            character.value.id,
            equipped.type,
            equipped.bonus,
            equipped.hasDexterityCap,
            equipped.dexterityCap,
            armor.proficiencies
        );
    }
}

async function toggleSkillPin(skill: Skill) {
    if (character.value !== undefined) {
        const skills = {} as Record<string, SkillEditDto>;
        skills[skill.name] = { pinned: !skill.pinned };

        await api.editSkills(character.value.id, skills);
    }
}

// TODO: Longtap for private roll or toggle somewhere for the roll type
async function rollSkillCheck(skillType: string) {
    if (character.value !== undefined) {
        const message = await api.performSkillCheck(character.value.id, skillType, RollType.Public);
        const toast = useToast();
        toast(
            {
                component: SkillCheckResultToast,
                props: {
                    message,
                },
            },
            {
                toastClassName: 'skill-check-toast',
            }
        );
    }
}

async function rollSavingThrow(savingThrow: string) {
    if (character.value !== undefined) {
        const message = await api.performSavingThrow(character.value.id, savingThrow, RollType.Public);
        const toast = useToast();
        toast(
            {
                component: SavingThrowResultToast,
                props: {
                    message,
                },
            },
            {
                toastClassName: 'saving-throw-toast',
            }
        );
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
    const headers = document.querySelectorAll('h2');
    const navigation = document.querySelectorAll('#sections-bar a');

    const sectionBar = document.querySelector('#sections-bar');

    if (sectionBar == undefined) {
        return;
    }

    // Find first header that hasn't reached the border
    const border = sectionBar.getBoundingClientRect().bottom;
    const start = headers.item(0).getBoundingClientRect().top;
    let sectionIndex = 0;

    if (border > start) {
        for (let i = 1; i < headers.length; i++) {
            const rect = headers.item(i).getBoundingClientRect();
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
    sectionIndex =
        sectionIndex == 0 || headers.item(sectionIndex).getBoundingClientRect().top - border <= 60
            ? sectionIndex
            : sectionIndex - 1;
    const target = headers.item(sectionIndex).innerText;

    // Update styleclass for each navigation button
    navigation.forEach((element) => {
        if (element.innerHTML == target) {
            element.classList.add('text-primary');
            element.classList.add('active');
        } else {
            element.classList.remove('text-primary');
            element.classList.remove('active');
        }
    });
}
</script>

<template>
    <div v-if="character !== undefined" class="flex flex-col max-h-full">
        <!--Header-->
        <div id="sections-bar"
            class="sticky bg-base-100 flex flew-row flex-nowrap min-h-fit overflow-auto no-scrollbar lg:hidden">
            <a v-for="section in sections" :key="section.link" :href="section.link"
                class="py-1 px-4 pb-0 grow text-nowrap tracking-tight border-b-2 border-base-300 select-none">{{
                    section.header }}</a>
        </div>

        <!--Character Sheet Content-->
        <div class="flex flex-col overflow-y-auto p-2 gap-2 scroll-smooth no-scrollbar bg-base-200"
            @scroll="updateSection">
            <AbilitiesWidget id="attributes" :abilities="character.abilities" @changed="updateAbilities" />
            <SavingThrowsWidget id="saving-throws" :saving-throws="character.savingThrows" @changed="updateSavingThrows"
                @roll="(type) => rollSavingThrow(type)" />
            <SkillsWidget id="skills" :skills="character.skills" @changed="updateSkills"
                @pin="(skill) => toggleSkillPin(skill)"
                @roll="(type) => rollSkillCheck(type)" />
            <ArmorWidget id="armor" :armor="character.armor" @changed="updateArmor" />
            <SpeedsWidget id="speeds" :speeds="character.speeds" @changed="updateSpeeds" />
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
