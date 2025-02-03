<script setup lang="ts">
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';

import type { DialogResultCallback } from '@/composables/useModal';

const { t } = useI18n();

const noComfort = ref(false);
const inArmor = ref(false);

const { closeModal } = defineProps<{
    closeModal: DialogResultCallback<{ inArmor: boolean; noComfort: boolean }>;
}>();

function confirm() {
    const payload = { inArmor: inArmor.value, noComfort: noComfort.value };
    closeModal({ action: 'result', payload });
}

function cancel() {
    closeModal({ action: 'reject' });
}
</script>

<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">{{ t('dialogs.longRest.header') }}</h3>
            <form method="dialog" @submit.prevent="confirm">
                <div class="flex flex-col gap-1">
                    <label class="label cursor-pointer">
                        <span class="label-text">{{ t('dialogs.longRest.noComfort') }}</span>
                        <input v-model="noComfort" type="checkbox" class="checkbox" />
                    </label>

                    <label class="label cursor-pointer">
                        <span class="label-text">{{ t('dialogs.longRest.inArmor') }}</span>
                        <input v-model="inArmor" type="checkbox" class="checkbox" />
                    </label>
                </div>
            </form>
            <div class="modal-action">
                <form method="dialog" class="space-x-2">
                    <button class="btn btn-success w-24" type="button" @click="confirm">
                        {{ t('dialogs.longRest.rest') }}
                    </button>
                    <button class="btn w-24" type="button" @click="cancel">{{ t('actions.cancel') }}</button>
                </form>
            </div>
        </div>
    </dialog>
</template>
