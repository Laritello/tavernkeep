<script setup lang="ts">
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';

import { type DialogResultCallback } from '@/composables/useModal';
import type { Speed } from '@/contracts/character';
import type { SpeedEditDto } from '@/contracts/dtos';
import { SpeedType } from '@/contracts/enums';

const { t } = useI18n();

type ReturnType = Record<SpeedType, SpeedEditDto>;
const { closeModal, speeds } = defineProps<{
    speeds: Record<SpeedType, Speed>;
    closeModal: DialogResultCallback<ReturnType>;
}>();

const convertedSpeeds = ref(Object.values(speeds).map((s) => ({ type: s.type, active: s.active, base: s.base })));

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
            <h3 class="font-bold text-lg">{{ t('dialogs.speedsEdit.header') }}</h3>

            <div class="flex flex-col gap-1">
                <div v-for="item in convertedSpeeds" :key="item.type">
                    <label class="form-control w-full max-w-xs">
                        <div class="label">
                            <span class="label-text text-clip text-nowrap">{{
                                t(`pf.speeds.${item.type.toLowerCase()}`)
                            }}</span>
                            <input v-model="item.active" type="checkbox" class="toggle toggle-xs" />
                        </div>
                        <input
                            v-model="item.base"
                            type="text"
                            class="input input-bordered w-full max-w-xs"
                            :disabled="!item.active"
                        />
                    </label>
                </div>
            </div>

            <form method="dialog" @submit.prevent="confirm">
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">{{ t('actions.save') }}</button>
                    <button class="btn w-24" type="button" @click="cancel">{{ t('actions.cancel') }}</button>
                </div>
            </form>
        </div>
    </dialog>
</template>
