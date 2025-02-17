<script setup lang="ts">
import { useI18n } from 'vue-i18n';

import ArmorEditDialog from '@/components/dialogs/edit/ArmorEditDialog.vue';
import { useModal } from '@/composables/useModal';
import type { Armor } from '@/contracts/character';
import { ArmorType } from '@/contracts/enums';

import ProficiencyComponent from '../../ProficiencyComponent.vue';

const { t } = useI18n();

const { armor } = defineProps<{
    armor: Armor;
}>();

const emits = defineEmits<{
    changed: [value: Armor];
}>();

const types: ArmorType[] = [ArmorType.Unarmored, ArmorType.Light, ArmorType.Medium, ArmorType.Heavy];

async function showEditAbilitiesDialog() {
    const modal = useModal();
    const result = await modal.show(ArmorEditDialog, {
        armor,
    });

    if (result.action !== 'result') {
        return;
    }

    emits('changed', result.payload);
}
</script>

<template>
    <div class="bg-base-100 border-2 border-base-300 rounded-xl flex flex-col p-2 w-full lg:max-w-md">
        <div class="flex flex-row justify-center items-center">
            <h2 class="mr-1 text-lg font-semibold select-none">{{ t('sections.armor') }}</h2>
            <button class="btn btn-sm btn-circle btn-ghost" @click="showEditAbilitiesDialog()">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="currentColor" viewBox="0 -960 960 960">
                    <path
                        d="m370-80-16-128q-13-5-24.5-12T307-235l-119 50L78-375l103-78q-1-7-1-13.5v-27q0-6.5 1-13.5L78-585l110-190 119 50q11-8 23-15t24-12l16-128h220l16 128q13 5 24.5 12t22.5 15l119-50 110 190-103 78q1 7 1 13.5v27q0 6.5-2 13.5l103 78-110 190-118-50q-11 8-23 15t-24 12L590-80H370Zm70-80h79l14-106q31-8 57.5-23.5T639-327l99 41 39-68-86-65q5-14 7-29.5t2-31.5q0-16-2-31.5t-7-29.5l86-65-39-68-99 42q-22-23-48.5-38.5T533-694l-13-106h-79l-14 106q-31 8-57.5 23.5T321-633l-99-41-39 68 86 64q-5 15-7 30t-2 32q0 16 2 31t7 30l-86 65 39 68 99-42q22 23 48.5 38.5T427-266l13 106Zm42-180q58 0 99-41t41-99q0-58-41-99t-99-41q-59 0-99.5 41T342-480q0 58 40.5 99t99.5 41Zm-2-140Z"
                    />
                </svg>
            </button>
        </div>

        <div class="flex flex-col">
            <div class="flex flex-row gap-1">
                <div class="box-border border-base-300 flex flex-col p-1 px-3 border-2 rounded-lg w-20">
                    <p class="text-sm text-center select-none">{{ t('widgets.armor.bonus') }}</p>
                    <p class="text-lg font-extrabold text-center select-none">{{ armor.equipped.bonus }}</p>
                </div>
                <div class="flex-1 box-border border-base-300 flex flex-col p-1 px-3 border-2 rounded-lg">
                    <p class="text-sm text-center select-none">{{ t('widgets.armor.type') }}</p>
                    <p class="text-lg font-extrabold text-center select-none uppercase tracking-tight">
                        {{ t(`pf.armor.${armor.equipped.type.toLowerCase()}`) }}
                    </p>
                </div>
                <div class="box-border border-base-300 flex flex-col p-1 px-3 border-2 rounded-lg w-20">
                    <p class="text-sm text-center select-none">{{ t('widgets.armor.cap') }}</p>
                    <p class="text-lg font-extrabold text-center select-none">
                        {{ armor.equipped.hasDexterityCap ? armor.equipped.dexterityCap : '-' }}
                    </p>
                </div>
            </div>

            <div class="collapse">
                <input type="checkbox" />
                <div class="collapse-title text-md font-medium px-5 pb-0">
                    <div class="divider uppercase">{{ t('widgets.armor.proficiencies') }}</div>
                </div>
                <div class="collapse-content">
                    <div class="flex flex-col">
                        <div
                            v-for="type in types"
                            :key="type"
                            class="flex flex-row items-center p-1 gap-x-2 border-b-2 border-base-300 proficiency-item"
                        >
                            <p class="grow select-none">{{ t(`pf.armor.${type.toLowerCase()}`) }}</p>
                            <ProficiencyComponent :proficiency="armor.proficiencies[type]" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.proficiency-item:last-child {
    border: none;
}
</style>
