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
</script>

<template>
    <dialog class="modal">
        <div class="modal-box">
            <StatblockComponent :statblock="data" />
            <div class="modal-action">
                <form method="dialog" class="space-x-2">
                    <button class="btn btn-error w-24" type="button" @click="confirm('confirm')">Yes</button>
                    <button class="btn w-24" type="button" @click="confirm('reject')">No</button>
                </form>
            </div>
        </div>
    </dialog>
</template>
