<script setup lang="ts">
import { type DialogResultCallback } from '@/composables/useModal';
import type { Speed } from '@/contracts/character';
import { SpeedType } from '@/contracts/enums';
import { useI18n } from 'vue-i18n';
import { ref } from 'vue';
import type { SpeedEditDto } from '@/contracts/dtos';

const { t } = useI18n();

type ReturnType = Record<SpeedType, SpeedEditDto>;
const { closeModal, speeds } = defineProps<{
    speeds: Record<SpeedType, Speed>;
    closeModal: DialogResultCallback<ReturnType>;
}>();

const convertedSpeeds = ref(Object
    .values(speeds)
    .map(s => ({ type: s.type, active: s.active, base: s.base })));

function confirm() {
    const payload = {} as Record<SpeedType, SpeedEditDto>;

    for (const item of convertedSpeeds.value) {
        payload[item.type] = { active: item.active, base: item.base } as SpeedEditDto;
    }

    closeModal({ action: 'result', payload });
}

function cancel() {
    closeModal({ action: 'reject' });
}
</script>

<template>
    <dialog class="modal">
        <div class="modal-box overflow-y-hidden">
            <h3 class="font-bold text-lg">{{ t('widgets.speeds.editDialog.header') }}</h3>

            <div class="flex flex-col gap-1">
                <div v-for="item in convertedSpeeds" :key="item.type">
                    <label class="form-control w-full max-w-xs">
                        <div class="label">
                            <span class="label-text text-clip text-nowrap">{{ t(`pf.speeds.${item.type.toLowerCase()}`) }}</span>
                            <input type="checkbox" class="toggle toggle-xs" v-model="item.active" />
                        </div>
                        <input type="text" class="input input-bordered w-full max-w-xs" v-bind:disabled="!item.active"
                            v-model="item.base" />
                    </label>
                </div>
            </div>

            <form @submit.prevent="confirm" method="dialog">
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">{{ t('actions.save') }}</button>
                    <button @click="cancel" class="btn w-24" type="button">{{ t('actions.cancel') }}</button>
                </div>
            </form>
        </div>
    </dialog>
</template>
