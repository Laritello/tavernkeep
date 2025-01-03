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
                    <button @click="confirm('confirm')" class="btn btn-error w-24" type="button">Yes</button>
                    <button @click="confirm('reject')" class="btn w-24" type="button">No</button>
                </form>
            </div>
        </div>
    </dialog>
</template>
