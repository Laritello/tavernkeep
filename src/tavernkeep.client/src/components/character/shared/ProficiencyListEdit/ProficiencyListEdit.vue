<script setup lang="ts">
import type { BaseSkill } from '@/contracts/character';
import ProficiencyListEditItem from './ProficiencyListEditItem.vue';

const items = defineModel<BaseSkill[]>({ required: true });

const { localePrefix } = defineProps<{
    localePrefix: string
}>();

</script>

<template>
    <div class="flex flex-col">
        <div class="flex flex-col">
            <ProficiencyListEditItem v-for="(item, index) in items" :key="item.name" 
                                     :item="item" 
                                     :locale-prefix="localePrefix" 
                                     @updated="(newValue) => items[index] = newValue" class="hover:bg-base-200" />
        </div>
    </div>
</template>

<style scoped></style>