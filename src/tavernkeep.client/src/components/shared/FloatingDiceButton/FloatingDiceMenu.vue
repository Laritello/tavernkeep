<script setup lang="ts">
import { ref, useTemplateRef } from 'vue';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import FloatingDiceMenuItem from '@/components/shared/FloatingDiceButton/FloatingDiceMenuItem.vue';
import { RollType } from '@/contracts/enums';

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
const buttonsRefs = useTemplateRef<InstanceType<typeof FloatingDiceMenuItem>[]>('buttons');
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
}
</script>

<template>
    <div class="menu flex flex-row gap-4 bg-base-200">
        <div class="flex flex-row gap-1">
            <FloatingDiceMenuItem
                ref="buttons"
                v-for="button in actionButtons"
                :dice="button.dice"
                :key="button.dice"
            />
        </div>
        <div class="flex flex-row gap-2 items-center">
            <button @click="modifier--" class="btn btn-circle btn-error btn-sm">-</button>
            <input v-model="modifier" class="input input-sm text-center w-16" placeholder="Modifier" />
            <button @click="modifier++" class="btn btn-circle btn-success btn-sm">+</button>
        </div>
        <button @click="rollDice()" class="btn btn-outline btn-md">Roll</button>
    </div>
</template>

<style scoped></style>
