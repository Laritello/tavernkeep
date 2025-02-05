<template>
    <div class="flex flex-col items-stretch content-stretch gap-6">
        <div>
            <h1 class="font-semibold text-slate-500 uppercase self-start px-1">
                {{ t('builder.stages.general.character.header') }}
            </h1>

            <label class="form-control w-full">
                <label class="label">
                    <span class="label-text">{{ t('builder.stages.general.character.name') }}</span>
                </label>
                <input
                    v-model="template.name"
                    type="text"
                    placeholder="Unknown hero"
                    class="input input-bordered w-full"
                />
            </label>

            <label class="form-control w-full">
                <label class="label">
                    <span class="label-text">{{ t('builder.stages.general.character.level') }}</span>
                </label>
                <input
                    v-model="template.level"
                    type="number"
                    placeholder="Unknown hero"
                    class="input input-bordered w-full"
                />
            </label>
        </div>

        <div>
            <h1 class="font-semibold text-slate-500 uppercase self-start px-1">
                {{ t('builder.stages.general.ancestry.header') }}
            </h1>

            <label class="form-control w-full">
                <label class="label">
                    <span class="label-text">{{ t('builder.stages.general.ancestry.name') }}</span>
                </label>
                <select v-model="ancestryName" class="select select-bordered w-full">
                    <option v-for="value in Object.keys(AncestryType)" :key="value">{{ value }}</option>
                </select>
            </label>

            <label class="form-control w-full">
                <label class="label">
                    <span class="label-text">{{ t('builder.stages.general.ancestry.health') }}</span>
                </label>
                <input
                    v-model="template.ancestry.health"
                    type="number"
                    placeholder="Type here"
                    class="input input-bordered w-full"
                />
            </label>
        </div>

        <div>
            <h1 class="font-semibold text-slate-500 uppercase self-start px-1">
                {{ t('builder.stages.general.class.header') }}
            </h1>

            <label class="form-control w-full">
                <label class="label">
                    <span class="label-text">{{ t('builder.stages.general.class.name') }}</span>
                </label>
                <select v-model="className" class="select select-bordered w-full">
                    <option v-for="value in Object.keys(ClassType)" :key="value">{{ value }}</option>
                </select>
            </label>

            <label class="form-control w-full">
                <label class="label">
                    <span class="label-text">{{ t('builder.stages.general.class.healthPerLevel') }}</span>
                </label>
                <input
                    v-model="template.class.healthPerLevel"
                    type="number"
                    placeholder="Type here"
                    class="input input-bordered w-full"
                />
            </label>
        </div>
    </div>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia';
import { useI18n } from 'vue-i18n';

import { AncestryType, ClassType } from '@/contracts/enums';

import { useCharacterBuilderStore } from '../stores/characterBuilderStore.ts';

const { t } = useI18n();

const characterBuilderStore = useCharacterBuilderStore();
const { template, ancestryName, className } = storeToRefs(characterBuilderStore);
</script>
