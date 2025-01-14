<script setup lang="ts">
import type { DialogResultCallback } from '@/composables/useModal';
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();

const noComfort = ref(false);
const inArmor = ref(false);

const { closeModal } = defineProps<{
    closeModal: DialogResultCallback<{ inArmor: boolean, noComfort: boolean }>;
}>();

function confirm() {
    let payload = { inArmor: inArmor.value, noComfort: noComfort.value };
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
            <form @submit.prevent="confirm" method="dialog">
                <div class="flex flex-col gap-1">
                    <label class="label cursor-pointer">
                        <span class="label-text">{{ t('dialogs.longRest.noComfort') }}</span>
                        <input type="checkbox" class="checkbox" v-model="noComfort"/>
                    </label>

                    <label class="label cursor-pointer">
                        <span class="label-text">{{ t('dialogs.longRest.inArmor') }}</span>
                        <input type="checkbox" class="checkbox" v-model="inArmor"/>
                    </label>
                </div>
            </form>
            <div class="modal-action">
                <form method="dialog" class="space-x-2">
                    <button @click="confirm" class="btn btn-success w-24" type="button">{{ t('dialogs.longRest.rest') }}</button>
                    <button @click="cancel" class="btn w-24" type="button">{{ t('actions.cancel') }}</button>
                </form>
            </div>
        </div>
    </dialog>
</template>
