<script setup lang="ts">
import { ref } from 'vue';
import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';
import { ApiClientFactory } from '@/factories/ApiClientFactory';

import BottomSheet from '@douxcode/vue-spring-bottom-sheet';



// Refs
const menu = ref<InstanceType<typeof BottomSheet>>();
const selectedSkillType = ref<string>('Basic');
const selectedBaseAbility = ref<string>('Intelligence');
const currentSkillName = ref<string>('');

async function create() {
    const api = ApiClientFactory.createApiClient();
    const { activeCharacter } = useCurrentUserAccount();
    if(!activeCharacter.value) {
        throw new Error('No active character');
    }
    
    const skillName = currentSkillName.value.trim();
    const skillType = selectedSkillType.value;
    let baseAbility = selectedBaseAbility.value;
    
    if (skillType === 'Lore') {
        baseAbility = 'Intelligence';
    }
    
    await api.createCustomSkill(activeCharacter.value.id, skillType, baseAbility, skillName);
    menu.value?.close();
}

function resetToDefault() {
    selectedSkillType.value = 'Basic';
    selectedBaseAbility.value = 'Intelligence';
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
        <form @submit.prevent="create" method="dialog" class="flex flex-col gap-2 mb-4">
            <label class="form-control w-full">
                <label class="label">
                    <span class="label-text text-xs">Select skill type</span>
                </label>
                <select v-model="selectedSkillType" class="select select-bordered">
                    <option value="Basic" selected>Basic</option>
                    <option value="Lore">Lore</option>
                </select>
            </label>

            <label v-if="selectedSkillType === 'Basic'" class="form-control w-full">
                <label class="label">
                    <span class="label-text text-xs">Select base ability</span>
                </label>
                <select v-model="selectedBaseAbility" class="select select-bordered">
                    <option value="Strength">Strength</option>
                    <option value="Dexterity">Dexterity</option>
                    <option value="Constitution">Constitution</option>
                    <option value="Intelligence" selected>Intelligence</option>
                    <option value="Wisdom">Wisdom</option>
                    <option value="Charisma">Charisma</option>
                </select>
            </label>
            <label class="form-control w-full">
                <label class="label">
                    <span class="label-text text-xs">Enter skill name</span>
                </label>
                <label class="input input-bordered flex items-center gap-2">
                    <template v-if="selectedSkillType === 'Lore'">Lore:</template>
                    <input v-model="currentSkillName" type="text" class="grow" placeholder="Name" required />
                </label>
            </label>
            <button type="submit" class="btn btn-success bg-base">
                Create
            </button>
        </form>
    </BottomSheet>
</template>

<style>
[data-theme="dark"] {
    --vsbs-background: oklch(25.3267% 0.015896 252.417568);
}
</style>