<script setup lang="ts">
import type { DialogResultCallback } from '@/composables/useModal';

import StatblockComponent from '../creature/StatblockComponent.vue';

const { closeModal, data } = defineProps<{
    data: string;
    closeModal: DialogResultCallback;
}>();

function confirm(choice: 'confirm' | 'reject') {
    closeModal({ action: choice });
}

function roll(type: string, name: string, value: string) {
    console.log(`Rolled ${type} with name ${name} and bonus ${value}`);
}
</script>

<template>
    <dialog class="modal">
        <div class="modal-box">
            <StatblockComponent :statblock="data" @rolled="roll" />
            <div class="modal-action">
                <form method="dialog" class="space-x-2">
                    <button class="btn w-24" type="button" @click="confirm('reject')">Close</button>
                </form>
            </div>
        </div>
    </dialog>
</template>
