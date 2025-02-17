﻿<script setup lang="ts">
import BottomSheet from '@douxcode/vue-spring-bottom-sheet';
import { ref } from 'vue';

import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';
import { SkillDataType } from '@/contracts/enums';
import { ApiClientFactory } from '@/factories/ApiClientFactory';

// Refs
const menu = ref<InstanceType<typeof BottomSheet>>();
const selectedSkillType = ref<SkillDataType>(SkillDataType.Custom);
const selectedBaseAbility = ref<string>('Intelligence');
const currentSkillName = ref<string>('');

async function create() {
    const api = ApiClientFactory.createApiClient();
    const { activeCharacter } = useCurrentUserAccount();
    if (!activeCharacter.value) {
        throw new Error('No active character');
    }

    const skillName = currentSkillName.value.trim();
    const skillType = selectedSkillType.value;
    let baseAbility = selectedBaseAbility.value;

    if (skillType === SkillDataType.Lore) {
        baseAbility = 'Intelligence';
    }

    await api.createSkill(activeCharacter.value.id, skillType, baseAbility, skillName);
    menu.value?.close();
}

function resetToDefault() {
    selectedSkillType.value = SkillDataType.Custom;
    selectedBaseAbility.value = 'Strength';
    currentSkillName.value = '';
}

defineExpose({
    open: () => menu.value?.open(),
    close: () => menu.value?.close(),
    snapToPoint: (point: number) => menu.value?.snapToPoint(point),
});
</script>

<template>
    <BottomSheet ref="menu" @closed="resetToDefault">
        <template #header>
            <h1 class="text-lg font-semibold">Create custom skill</h1>
        </template>
        <form method="dialog" class="flex flex-col gap-2 mb-4" @submit.prevent="create">
            <label class="form-control w-full">
                <label class="label">
                    <span class="label-text text-xs">Skill type</span>
                </label>
                <select v-model="selectedSkillType" class="select select-bordered">
                    <option :value="SkillDataType.Custom" selected>{{ SkillDataType.Custom.toString() }}</option>
                    <option :value="SkillDataType.Lore">{{ SkillDataType.Lore.toString() }}</option>
                </select>
            </label>

            <label v-if="selectedSkillType === SkillDataType.Custom" class="form-control w-full">
                <label class="label">
                    <span class="label-text text-xs">Base ability</span>
                </label>
                <select v-model="selectedBaseAbility" class="select select-bordered">
                    <option value="Strength" selected>{{ $t('pf.attributes.strength') }}</option>
                    <option value="Dexterity">{{ $t('pf.attributes.dexterity') }}</option>
                    <option value="Constitution">{{ $t('pf.attributes.constitution') }}</option>
                    <option value="Intelligence">{{ $t('pf.attributes.intelligence') }}</option>
                    <option value="Wisdom">{{ $t('pf.attributes.wisdom') }}</option>
                    <option value="Charisma">{{ $t('pf.attributes.charisma') }}</option>
                </select>
            </label>
            <label class="form-control w-full">
                <label class="label">
                    <span class="label-text text-xs">Skill name</span>
                </label>
                <label class="input input-bordered flex items-center gap-2">
                    <template v-if="selectedSkillType === SkillDataType.Lore">Lore:</template>
                    <input v-model="currentSkillName" type="text" class="grow" placeholder="Name" required />
                </label>
            </label>
            <button type="submit" class="btn btn-success bg-base">Create</button>
        </form>
    </BottomSheet>
</template>
