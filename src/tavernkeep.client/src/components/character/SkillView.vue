<template>
    <div class="flex flex-row items-center p-1 gap-x-2 border-b-2">

        <label class="swap">
            <!-- this hidden checkbox controls the state -->
            <input type="checkbox" />

            <!--If favourite disabled-->
            <svg xmlns="http://www.w3.org/2000/svg" class="swap-off w-4 h-4" viewBox="0 -960 960 960"
                fill="currentColor">
                <path
                    d="m640-480 80 80v80H520v240l-40 40-40-40v-240H240v-80l80-80v-280h-40v-80h400v80h-40v280Zm-286 80h252l-46-46v-314H400v314l-46 46Zm126 0Z" />
            </svg>
            
            <!--If favourite enabled-->
            <svg xmlns="http://www.w3.org/2000/svg" class="swap-on w-4 h-4" viewBox="0 -960 960 960"
                fill="currentColor">
                <path
                    d="M680-840v80h-40v327l-80-80v-247H400v87l-87-87-33-33v-47h400ZM480-40l-40-40v-240H240v-80l80-80v-46L56-792l56-56 736 736-58 56-264-264h-6v240l-40 40ZM354-400h92l-44-44-2-2-46 46Zm126-193Zm-78 149Z" />
            </svg>
        </label>

        <p class="grow select-none">{{ skill.type }}</p>
        <ProficiencyComponent :proficiency="skill.proficiency" />
        <p class="border-2 rounded-md w-12 text-center active:bg-gray-400 select-none" @click="emit('roll', skill)">{{
            skill.bonus }}</p>
    </div>
</template>

<script lang="ts" setup>
import type { Skill } from '@/contracts/character';
import ProficiencyComponent from './ProficiencyComponent.vue';

const { skill } = defineProps<{
    skill: Skill;
}>();

const emit = defineEmits<{
    roll: [Skill];
}>();
</script>
<style></style>
