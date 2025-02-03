<script setup lang="ts">
import { useI18n } from 'vue-i18n';

import type { DialogResultCallback } from '@/composables/useModal';
import type { CharacterInformationEditDto } from '@/contracts/dtos';
import type { Character } from '@/entities';

const { t } = useI18n();

const { character, closeModal } = defineProps<{
    character: Character;
    closeModal: DialogResultCallback<CharacterInformationEditDto>;
}>();

const information = {
    name: character.name,
    ancestry: character.ancestry.name,
    class: character.class.name,
    level: character.level,
} as CharacterInformationEditDto;

function confirm() {
    const payload = information;
    closeModal({ action: 'result', payload });
}

function cancel() {
    closeModal({ action: 'reject' });
}
</script>

<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">{{ t('dialogs.informationEdit.header') }}</h3>
            <form method="dialog" class="space-x-2" @submit.prevent="confirm">
                <div class="flex flex-col gap-1">
                    <label class="form-control w-full max-w-xs">
                        <div class="label">
                            <span class="label-text">{{ t('dialogs.informationEdit.name') }}</span>
                        </div>
                        <input
                            v-model="information.name"
                            type="text"
                            placeholder="Type here"
                            class="input input-bordered w-full max-w-xs"
                        />
                    </label>

                    <label class="form-control w-full max-w-xs">
                        <div class="label">
                            <span class="label-text">{{ t('dialogs.informationEdit.level') }}</span>
                        </div>
                        <input
                            v-model="information.level"
                            type="text"
                            placeholder="Type here"
                            class="input input-bordered w-full max-w-xs"
                        />
                    </label>

                    <label class="form-control w-full max-w-xs">
                        <div class="label">
                            <span class="label-text">{{ t('dialogs.informationEdit.ancestry') }}</span>
                        </div>
                        <input
                            v-model="information.ancestry"
                            type="text"
                            placeholder="Type here"
                            class="input input-bordered w-full max-w-xs"
                        />
                    </label>

                    <label class="form-control w-full max-w-xs">
                        <div class="label">
                            <span class="label-text">{{ t('dialogs.informationEdit.class') }}</span>
                        </div>
                        <input
                            v-model="information.class"
                            type="text"
                            placeholder="Type here"
                            class="input input-bordered w-full max-w-xs"
                        />
                    </label>
                </div>
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">{{ t('actions.save') }}</button>
                    <button class="btn w-24" type="button" @click="cancel">{{ t('actions.cancel') }}</button>
                </div>
            </form>
        </div>
    </dialog>
</template>

<style scoped></style>
