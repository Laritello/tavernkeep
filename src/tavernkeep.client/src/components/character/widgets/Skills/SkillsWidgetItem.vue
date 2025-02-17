<template>
    <div class="flex flex-row items-center p-1 gap-x-2 border-b-2 border-base-300">
        <label class="swap">
            <!-- this hidden checkbox controls the state -->
            <input type="checkbox" name="input-skill-pin" :checked="skill.pinned" @click="emit('pin', skill)" />

            <!--If favourite disabled-->
            <svg
                xmlns="http://www.w3.org/2000/svg"
                class="swap-off w-4 h-4"
                viewBox="0 -960 960 960"
                fill="currentColor"
            >
                <path
                    d="m640-480 80 80v80H520v240l-40 40-40-40v-240H240v-80l80-80v-280h-40v-80h400v80h-40v280Zm-286 80h252l-46-46v-314H400v314l-46 46Zm126 0Z"
                />
            </svg>

            <!--If favourite enabled-->
            <svg
                xmlns="http://www.w3.org/2000/svg"
                class="swap-on w-4 h-4"
                viewBox="0 -960 960 960"
                fill="currentColor"
            >
                <path
                    d="M640-760v280l68 68q6 6 9 13.5t3 15.5v23q0 17-11.5 28.5T680-320H520v234q0 17-11.5 28.5T480-46q-17 0-28.5-11.5T440-86v-234H280q-17 0-28.5-11.5T240-360v-23q0-8 3-15.5t9-13.5l68-68v-280q-17 0-28.5-11.5T280-800q0-17 11.5-28.5T320-840h320q17 0 28.5 11.5T680-800q0 17-11.5 28.5T640-760Z"
                />
            </svg>
        </label>

        <p class="grow select-none">{{ getSkillName(skill) }}</p>
        <ProficiencyComponent :proficiency="skill.proficiency" />
        <p
            class="border-2 border-base-300 rounded-md w-12 text-center font-bold active:bg-gray-400 select-none"
            @click="emit('roll', skill)"
        >
            {{ skill.bonus }}
        </p>
    </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n';

import ProficiencyComponent from '@/components/character/ProficiencyComponent.vue';
import type { Skill } from '@/contracts/character';
import { SkillDataType } from '@/contracts/enums';

const { t } = useI18n();

const { skill } = defineProps<{
    skill: Skill;
}>();

const emit = defineEmits<{
    roll: [Skill];
    pin: [Skill];
}>();

function getSkillName(skill: Skill): string {
    switch (skill.type) {
        case SkillDataType.Basic:
            return t(`pf.skills.${skill.name.toLowerCase()}`);
        case SkillDataType.Lore:
            return `${t('widgets.skills.lore')}: ${skill.name}`;
    }

    return skill.name;
}
</script>
<style></style>
