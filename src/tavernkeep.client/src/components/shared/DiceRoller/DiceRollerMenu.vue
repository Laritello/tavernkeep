<script setup lang="ts">
import BottomSheet from '@douxcode/vue-spring-bottom-sheet';
import { ref, useTemplateRef } from 'vue';
import { useI18n } from 'vue-i18n';

import { RollType } from '@/contracts/enums';
import { ApiClientFactory } from '@/factories/ApiClientFactory';

import DiceRollerMenuItem from './DiceRollerMenuItem.vue';

const { t } = useI18n();
const api = ApiClientFactory.createApiClient();
type diceType = 'd4' | 'd6' | 'd8' | 'd10' | 'd12' | 'd20';
const actionButtons = [
    { dice: 'd4' as diceType },
    { dice: 'd6' as diceType },
    { dice: 'd8' as diceType },
    { dice: 'd10' as diceType },
    { dice: 'd12' as diceType },
    { dice: 'd20' as diceType },
];

const menu = ref<InstanceType<typeof BottomSheet>>();
const buttonsRefs = useTemplateRef<InstanceType<typeof DiceRollerMenuItem>[]>('buttons');
const modifier = ref(0);
async function rollDice() {
    const sign = modifier.value > 0 ? '+' : '';
    const rollExpression =
        buttonsRefs
            .value!.filter((button) => button.count > 0)
            .map((button) => `${button.count}${button.dice}`)
            .join('+') + (modifier.value != 0 ? `${sign}${modifier.value}` : '');

    if (!rollExpression) return;
    buttonsRefs.value!.forEach((button) => button.reset());
    modifier.value = 0;

    await api.sendRollMessage(rollExpression, RollType.Public);
    menu.value?.close();
}

function resetToDefault() {
    buttonsRefs.value!.forEach((button) => button.reset());
    modifier.value = 0;
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
            <div class="flex flex-row justify-between items-center">
                <h1 class="text-xl font-semibold">{{ t('diceRoller.header') }}</h1>
            </div>
        </template>
        <div class="menu flex flex-row gap-4">
            <div class="flex flex-row gap-1">
                <DiceRollerMenuItem
                    ref="buttons"
                    v-for="button in actionButtons"
                    :dice="button.dice"
                    :key="button.dice"
                />
            </div>
            <div class="flex flex-row gap-2 items-center">
                {{ t('diceRoller.modifier') }}:
                <button @click="modifier--" class="btn btn-circle btn-error btn-sm">-</button>
                <input
                    v-model="modifier"
                    class="input input-bordered input-sm text-center w-16"
                    :placeholder="t('diceRoller.modifier')"
                />
                <button @click="modifier++" class="btn btn-circle btn-success btn-sm">+</button>
            </div>
            <button @click="rollDice()" class="btn btn-outline btn-circle btn-md">{{ t('diceRoller.roll') }}</button>
        </div>
    </BottomSheet>
</template>

<style scoped></style>
