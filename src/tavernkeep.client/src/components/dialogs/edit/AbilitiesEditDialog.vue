<script setup lang="ts">
import type { Ability } from '@/contracts/character';
import { type AbilityType } from '@/contracts/enums';
import type { DialogResultCallback } from '@/composables/useModal';
import SwipeNumericInput from '@/components/shared/SwipeNumericInput.vue';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();

const { abilities, closeModal } = defineProps<{ 
    abilities: Record<AbilityType, Ability>, 
    closeModal: DialogResultCallback<Record<AbilityType, Ability>> 
}>();

const currentItems = Object.values(abilities).reduce((acc, ability) => {
    acc[ability.type] = { ...ability };
    return acc;
}, {} as Record<AbilityType, Ability>);

function confirm() {
    const payload = currentItems;
    closeModal({ action: 'result', payload });
}

function cancel() {
    closeModal({ action: 'reject' });
}
</script>

<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">{{ t('widgets.attributes.editDialog.header') }}</h3>
            <form @submit.prevent="confirm" method="dialog" class="space-x-2">
                <div class="grid grid-cols-3">
                    <template v-for="ability in currentItems" :key="ability.type">
                        <div class="box-border flex flex-col m-2 p-2 border-2 rounded-lg">
                            <SwipeNumericInput :ability="ability"
                                               :max="20"
                                               :min="8"
                                               :swipe-sensitivity="0.9"
                                               @changed="(value) => currentItems[ability.type].score = value" />
                            <p class="text-sm font-light text-center select-none">{{ t(`pf.attributes.${ability.type.toLowerCase()}`) }}</p>
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

<style scoped>

</style>