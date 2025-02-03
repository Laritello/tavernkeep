<template>
    <div class="flex flex-col h-full p-2">
        <div class="h-full overflow-y-auto">
            <component :is="currentStage?.display" :character="character" @update="updateCharacter">
                Unknown stage
            </component>
        </div>

        <div class="grid grid-cols-2 pt-2">
            <button v-if="previousStage !== undefined" class="btn justify-self-start" @click="moveToPreviousStage">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 fill-current" viewBox="0 -960 960 960">
                    <path d="M560-240 320-480l240-240 56 56-184 184 184 184-56 56Z" />
                </svg>

                <div class="flex flex-col items-start">
                    <span class="text-xs font-normal md:block">{{ t('builder.actions.back') }}</span>
                    <span>{{ t(`builder.stages.${previousStage.name}.name`) }}</span>
                </div>
            </button>

            <button
                v-if="nextStage !== undefined"
                class="btn btn-neutral justify-self-end col-start-2"
                @click="moveToNextStage"
            >
                <div class="flex flex-col items-end">
                    <span class="text-xs font-normal md:block">{{ t('builder.actions.next') }}</span>
                    <span>{{ t(`builder.stages.${nextStage.name}.name`) }}</span>
                </div>

                <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 fill-current" viewBox="0 -960 960 960">
                    <path d="M504-480 320-664l56-56 240 240-240 240-56-56 184-184Z" />
                </svg>
            </button>

            <button
                v-if="nextStage == undefined"
                class="btn btn-primary justify-self-end col-start-2"
                @click="createCharacter"
            >
                <div class="flex flex-col items-end">
                    <span>{{ t('builder.actions.create') }}</span>
                </div>

                <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 fill-current" viewBox="0 -960 960 960">
                    <path
                        d="M360-720h80v-80h-80v80Zm160 0v-80h80v80h-80ZM360-400v-80h80v80h-80Zm320-160v-80h80v80h-80Zm0 160v-80h80v80h-80Zm-160 0v-80h80v80h-80Zm160-320v-80h80v80h-80Zm-240 80v-80h80v80h-80ZM200-160v-640h80v80h80v80h-80v80h80v80h-80v320h-80Zm400-320v-80h80v80h-80Zm-160 0v-80h80v80h-80Zm-80-80v-80h80v80h-80Zm160 0v-80h80v80h-80Zm80-80v-80h80v80h-80Z"
                    />
                </svg>
            </button>
        </div>
    </div>
</template>

<script setup lang="ts">
import { computed, ref, watch, type Component } from 'vue';
import { useI18n } from 'vue-i18n';

import type { Character } from '@/entities';
import type { KeyValue } from '@/types';

import CharacterBuilderStageAbilities from './stages/CharacterBuilderStageAbilities.vue';
import CharacterBuilderStageGeneral from './stages/CharacterBuilderStageGeneral.vue';
import CharacterBuilderStageSavingThrows from './stages/CharacterBuilderStageSavingThrows.vue';
import CharacterBuilderStageSkills from './stages/CharacterBuilderStageSkills.vue';
import CharacterBuilderStageWelcome from './stages/CharacterBuilderStageWelcome.vue';

const { t } = useI18n();

interface Stage {
    order: number;
    name: string;
    display: Component;
}

const stages: Stage[] = [
    {
        order: 1,
        name: 'welcome',
        display: CharacterBuilderStageWelcome,
    },
    {
        order: 2,
        name: 'general',
        display: CharacterBuilderStageGeneral,
    },
    {
        order: 3,
        name: 'abilities',
        display: CharacterBuilderStageAbilities,
    },
    {
        order: 4,
        name: 'skills',
        display: CharacterBuilderStageSkills,
    },
    {
        order: 5,
        name: 'savingThrows',
        display: CharacterBuilderStageSavingThrows,
    },
];

const currentStageIndex = ref<number>(1);
const currentStage = computed(() => stages.find((x) => x.order === currentStageIndex.value));
const previousStage = computed(() => stages.find((x) => x.order === currentStageIndex.value - 1));
const nextStage = computed(() => stages.find((x) => x.order === currentStageIndex.value + 1));

const emits = defineEmits<{
    updatedStage: [value: string | undefined];
    updateCharacter: [value: KeyValue<Character>];
    create: [];
}>();

const { character } = defineProps<{
    character: Character;
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

function createCharacter() {
    emits('create');
}

function updateCharacter(pair: KeyValue<Character>) {
    emits('updateCharacter', pair);
}
</script>
