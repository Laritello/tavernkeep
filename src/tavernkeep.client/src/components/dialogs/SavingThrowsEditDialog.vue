<script setup lang="ts">
import { ref } from 'vue';
import { type DialogResultCallback } from '@/composables/useModal';
import type { SavingThrow } from '@/contracts/character';
import { Proficiency, SavingThrowType } from '@/contracts/enums';
import SavingThrowsEditView from '@/components/character/SavingThrowsEditView.vue';

const { closeModal, savingThrows } = defineProps<{
    savingThrows: Record<SavingThrowType, SavingThrow>;
    closeModal: DialogResultCallback<Record<SavingThrowType, Proficiency>>;
}>();

const currentSavingThrows = ref({ ...savingThrows });

function confirm() {
    const payload = {  } as Record<SavingThrowType, Proficiency>;
    for (const savingThrow of Object.values(currentSavingThrows.value)) {
        payload[savingThrow.type] = savingThrow.proficiency;
    }
    closeModal({ action: 'result', payload });
}

function cancel() {
    closeModal({ action: 'reject' });
}

function updateSavingThrow(savingThrow: SavingThrow) {
    currentSavingThrows.value[savingThrow.type] = savingThrow;
}
</script>

<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">Edit skills</h3>
            <form @submit.prevent="confirm" method="dialog" class="space-x-2">
                <div class="flex flex-col w-full">
                    <div class="flex flex-col">
                        <SavingThrowsEditView v-for="savingThrow in Object.values(currentSavingThrows)" :savingThrow="savingThrow" :key="savingThrow.type" @onChange="updateSavingThrow" />
                    </div>
                </div>
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">Save</button>
                    <button @click="cancel" class="btn w-24" type="button">Cancel</button>
                </div>
            </form>
        </div>
    </dialog>
</template>
