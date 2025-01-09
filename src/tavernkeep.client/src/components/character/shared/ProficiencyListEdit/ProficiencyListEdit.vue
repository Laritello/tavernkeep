<script setup lang="ts">
import type { ProficiencyEditItemType } from './ProficiencyEditItemType';
import ProficiencyListEditItem from './ProficiencyListEditItem.vue';

const items = defineModel<ProficiencyEditItemType[]>({ required: true });

const { localePrefix } = defineProps<{
    localePrefix: string
}>();

</script>

<template>
    <div class="flex flex-col w-full">
        <div class="flex flex-col">
            <ProficiencyListEditItem v-for="(item, index) in items" :key="item.name" :item="item" :locale-prefix="localePrefix" @updated="(newValue) => items[index] = newValue" />
        </div>
    </div>
</template>

<style scoped></style>