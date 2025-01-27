<template>
    <div class="flex flex-col h-full p-2">
        <div class="h-full overflow-y-auto">
            <component :is="currentStage?.display" :character="character">
                Unknown stage
            </component>
        </div>

        <div class="grid grid-cols-2 pt-2">
            <button v-if="previousStage !== undefined" class="btn justify-self-start" @click="moveToPreviousStage">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 fill-current" viewBox="0 -960 960 960">
                    <path d="M560-240 320-480l240-240 56 56-184 184 184 184-56 56Z" />
                </svg>

                <div class="flex flex-col items-start">
                    <span class="text-base-content/50 text-xs font-normal md:block">Back</span>
                    <span>{{ previousStage.name }}</span>
                </div>
            </button>

            <button v-if="nextStage !== undefined" class="btn btn-neutral justify-self-end col-start-2"
                @click="moveToNextStage">
                <div class="flex flex-col items-end">
                    <span class="text-base-content/50 text-xs font-normal md:block">Next</span>
                    <span>{{ nextStage.name }}</span>
                </div>

                <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 fill-current" viewBox="0 -960 960 960">
                    <path d="M504-480 320-664l56-56 240 240-240 240-56-56 184-184Z" />
                </svg>
            </button>
        </div>
    </div>
</template>

<script setup lang="ts">
import { computed, ref, watch, type Component } from 'vue';
import CharacterBuilderStageAbilities from './stages/CharacterBuilderStageAbilities.vue';
import CharacterBuilderStageGeneral from './stages/CharacterBuilderStageGeneral.vue';
import CharacterBuilderStageSavingThrows from './stages/CharacterBuilderStageSavingThrows.vue';
import CharacterBuilderStageSkills from './stages/CharacterBuilderStageSkills.vue';
import type { Character } from '@/entities';
import CharacterBuilderStageWelcome from './stages/CharacterBuilderStageWelcome.vue';

interface Stage {
    order: number;
    name: string;
    display: Component;
}

const stages: Stage[] = [
    {
        order: 1,
        name: "Welcome",
        display: CharacterBuilderStageWelcome
    },
    {
        order: 2,
        name: "General",
        display: CharacterBuilderStageGeneral
    },
    {
        order: 3,
        name: "Abilities",
        display: CharacterBuilderStageAbilities
    },
    {
        order: 4,
        name: "Skills",
        display: CharacterBuilderStageSkills
    },
    {
        order: 5,
        name: "Saving Throws",
        display: CharacterBuilderStageSavingThrows
    },
]

const currentStageIndex = ref<number>(1);
const currentStage = computed(() => stages.find(x => x.order === currentStageIndex.value));
const previousStage = computed(() => stages.find(x => x.order === (currentStageIndex.value - 1)));
const nextStage = computed(() => stages.find(x => x.order === (currentStageIndex.value + 1)));

const emits = defineEmits<{
    updatedStage: [value: string | undefined]
}>();

const { character } = defineProps<{
    character: Character
}>();

watch(currentStage, (newValue) => {
    emits('updatedStage', newValue?.name);
});

function moveToPreviousStage() {
    if (currentStageIndex.value == 1) {
        return;
    }

    currentStageIndex.value = currentStageIndex.value - 1;
}

function moveToNextStage() {
    if (currentStageIndex.value == stages.length) {
        return;
    }

    currentStageIndex.value = currentStageIndex.value + 1;
}

</script>
