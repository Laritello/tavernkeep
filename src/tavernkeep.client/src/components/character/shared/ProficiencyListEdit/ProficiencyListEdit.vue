<script setup lang="ts" generic="T extends SkillLike">
import type { Proficiency, SkillDataType } from '@/contracts/enums';

import ProficiencyListEditItem from './ProficiencyListEditItem.vue';

export type SkillLike = {
    type: SkillDataType;
    name: string;
    proficiency: Proficiency;
};

const items = defineModel<T[]>({ required: true });

const { localePrefix } = defineProps<{
    localePrefix: string;
}>();

const emits = defineEmits<{
    updated: [value: T[]];
}>();

function update(index: number, value: T) {
    items.value[index] = value;
    emits('updated', items.value);
}
</script>

<template>
    <div class="flex flex-col">
        <div class="flex flex-col">
            <ProficiencyListEditItem
                v-for="(item, index) in items"
                :key="item.name"
                :item="item"
                :locale-prefix="localePrefix"
                class="hover:bg-base-200"
                @updated="(newValue) => update(index, newValue)"
            />
        </div>
    </div>
</template>

<style scoped></style>
