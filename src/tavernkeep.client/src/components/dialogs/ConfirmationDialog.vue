<script setup lang="ts">
import type { DialogResultCallback } from '@/composables/useModal';

const { closeModal, caption, message } = defineProps<{
    caption: string;
    message: string;
    closeModal: DialogResultCallback;
}>();

function confirm(choice: 'confirm' | 'reject') {
    closeModal({ action: choice });
}
</script>

<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">{{ caption }}</h3>
            <p class="py-4">{{ message }}</p>
            <div class="modal-action">
                <form method="dialog" class="space-x-2">
                    <button class="btn btn-error w-24" type="button" @click="confirm('confirm')">Yes</button>
                    <button class="btn w-24" type="button" @click="confirm('reject')">No</button>
                </form>
            </div>
        </div>
    </dialog>
</template>
