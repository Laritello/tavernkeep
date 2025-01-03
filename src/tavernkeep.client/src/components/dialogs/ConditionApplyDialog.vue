<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">Apply condition</h3>
            <form @submit.prevent="save" method="dialog" class="space-x-2">
                <select v-model="selected" class="select select-bordered border w-full max-w-xs">
                    <option disabled selected :value="undefined">Select condition</option>
                    <option v-for="condition in conditions" :key="condition.name" :value="condition">
                        {{ condition.name }}
                    </option>
                </select>

                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">Save</button>
                    <button @click="cancel" class="btn w-24" type="button">Cancel</button>
                </div>
            </form>
        </div>
    </dialog>
</template>
<script setup lang="ts">
import { type DialogResultCallback } from '@/composables/useModal';
import type { Condition } from '@/entities';

const { conditions, closeModal } = defineProps<{
    conditions: Condition[];
    closeModal: DialogResultCallback<Condition>;
}>();

var selected: Condition | undefined = undefined;

async function save() {
    if (selected === undefined) {
        closeModal({ action: 'reject' });
        return;
    }

    closeModal({ action: 'result', payload: selected });
}

function cancel() {
    closeModal({ action: 'reject' });
}
</script>
