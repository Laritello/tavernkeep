<script setup lang="ts">
import { ref } from 'vue';
import { VueScrollPicker } from 'vue-scroll-picker';
import type { DialogResultCallback } from '@/composables/useModal';
import "vue-scroll-picker/lib/style.css";
const generateArray = (start: number, end: number): number[] => {
    return Array.from({ length: end - start + 1 }, (_, i) => start + i);
};

const options = generateArray(-100, 100).map(number => ({
    name: number.toString(),
    value: number,
}));

type ResultType = { type: 'current' | 'temporary', amount: number };
const { closeModal } = defineProps<{
    closeModal: DialogResultCallback<ResultType>;
}>();

const selected = ref(0);


function applyCurrent() {
    const payload: ResultType = {
        type: 'current',
        amount: selected.value
    };
    closeModal({ action: 'result', payload });
}

function applyTemporary() {
    const payload: ResultType = {
        type: 'temporary',
        amount: selected.value
    };
    closeModal({ action: 'result', payload });
}

function cancel() {
    closeModal({ action: 'reject' });
}

</script>

<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">{{ $t('dialogs.healthEdit.header') }}</h3>
            <form method="dialog" class="space-x-2">
                <VueScrollPicker v-model="selected" :options="options" />
                <div class="modal-action">
                    <button @click="applyCurrent()" class="btn btn-success w-24" type="submit">Apply</button>
                    <button @click="applyTemporary()" :disabled="selected < 0" class="btn btn-success w-24" type="submit">Temporary</button>
                    <button @click="cancel" class="btn w-24" type="button">{{ $t('actions.cancel') }}</button>
                </div>
            </form>
        </div>
    </dialog>
</template>

<style src="vue-scroll-picker/lib/style.css">

</style>