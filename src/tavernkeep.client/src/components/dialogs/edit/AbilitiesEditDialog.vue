<script setup lang="ts">
import { useI18n } from 'vue-i18n';

import SwipeNumericInput from '@/components/shared/SwipeNumericInput.vue';
import type { DialogResultCallback } from '@/composables/useModal';
import type { Ability } from '@/contracts/character';

const { t } = useI18n();

const { abilities, closeModal } = defineProps<{
    abilities: Ability[];
    closeModal: DialogResultCallback<Record<string, number>>;
}>();

const currentItems = abilities.map((x) => ({ ...x }) as Ability);

function confirm() {
    const payload = currentItems.reduce(
        (acc, ability) => {
            acc[ability.name] = ability.score;
            return acc;
        },
        {} as Record<string, number>
    );

    closeModal({ action: 'result', payload });
}

function cancel() {
    closeModal({ action: 'reject' });
}
</script>

<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">{{ t('dialogs.abilitiesEdit.header') }}</h3>
            <form @submit.prevent="confirm" method="dialog" class="space-x-2">
                <div class="grid grid-cols-3">
                    <template v-for="ability in currentItems" :key="ability.type">
                        <div class="box-border flex flex-col m-2 p-2 border-2 rounded-lg">
                            <SwipeNumericInput
                                :ability="ability"
                                :max="20"
                                :min="8"
                                :swipe-sensitivity="0.9"
                                @changed="(value) => (ability.score = value)"
                            />
                            <p class="text-sm font-light text-center select-none">
                                {{ t(`pf.attributes.${ability.name.toLowerCase()}`) }}
                            </p>
                        </div>
                    </template>
                </div>
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">{{ t('actions.save') }}</button>
                    <button @click="cancel" class="btn w-24" type="button">{{ t('actions.cancel') }}</button>
                </div>
            </form>
        </div>
    </dialog>
</template>

<style scoped></style>
