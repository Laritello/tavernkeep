<template>
    <div class="flex flex-col h-full p-2">
        <div class="flex-1">
            <component :is="currentStage">
                Unknown stage
            </component>
        </div>

        <div class="flex justify-between">
            <button class="btn">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24"
                    stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                        d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />
                </svg>
                <div class="flex flex-col items-start">
                    <span class="text-base-content/50 hidden text-xs font-normal md:block">Back</span>
                    <span>General</span>
                </div>
            </button>
            <button class="btn btn-neutral">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24"
                    stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                        d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />
                </svg>
                Neutral
            </button>
        </div>
    </div>
</template>

<script setup lang="ts">
import { computed, ref, type Component } from 'vue';
import CharacterBuilderStageAbilities from './stages/CharacterBuilderStageAbilities.vue';
import CharacterBuilderStageGeneral from './stages/CharacterBuilderStageGeneral.vue';
import CharacterBuilderStageSavingThrows from './stages/CharacterBuilderStageSavingThrows.vue';
import CharacterBuilderStageSkills from './stages/CharacterBuilderStageSkills.vue';

interface Stage {
    order: number;
    name: string;
    display: Component;
}

const stages: Stage[] =
    [
        {
            order: 1,
            name: "General",
            display: CharacterBuilderStageGeneral
        },
        {
            order: 2,
            name: "Abilities",
            display: CharacterBuilderStageAbilities
        },
        {
            order: 3,
            name: "Skills",
            display: CharacterBuilderStageSkills
        },
        {
            order: 4,
            name: "Saving Throws",
            display: CharacterBuilderStageSavingThrows
        },
    ]

const currentStep = ref<number>(1);
const currentStage = computed(() => stages.find(x => x.order === currentStep.value)?.display);

</script>
