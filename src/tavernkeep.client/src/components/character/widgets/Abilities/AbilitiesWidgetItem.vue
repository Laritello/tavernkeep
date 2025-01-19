<template>
    <div class="box-border border-base-300 flex flex-col m-2 p-1 border-2 rounded-lg red">
        <p class="text-3xl font-extrabold text-center select-none">{{ ability.modifier }}</p>
        <p class="text-xs text-center select-none">{{ t(`pf.attributes.${ability.name.toLowerCase()}`) }}</p>
    </div>
</template>
<script setup lang="ts">
import type { Ability } from '@/contracts/character';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();

const { ability } = defineProps<{ ability: Ability }>();
</script>

<style>
.align-right {
    display: flex;
    justify-content: right;
    align-items: flex-end;
}
</style>
