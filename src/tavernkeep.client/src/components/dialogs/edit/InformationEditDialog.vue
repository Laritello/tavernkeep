<script setup lang="ts">
import type { DialogResultCallback } from '@/composables/useModal';
import { useI18n } from 'vue-i18n';
import type { Character } from '@/entities';
import type { CharacterInformationEditDto } from '@/contracts/dtos';

const { t } = useI18n();

const { character, closeModal } = defineProps<{
    character: Character,
    closeModal: DialogResultCallback<CharacterInformationEditDto>
}>();

const information = { name: character.name, ancestry: character.ancestry.name, class: character.class.name, level: character.level } as CharacterInformationEditDto;

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
            <form @submit.prevent="confirm" method="dialog" class="space-x-2">
                <div class="flex flex-col gap-1">
                    <label class="form-control w-full max-w-xs">
                        <div class="label">
                            <span class="label-text">{{ t('dialogs.informationEdit.name') }}</span>
                        </div>
                        <input type="text" placeholder="Type here" class="input input-bordered w-full max-w-xs"
                            v-model="information.name" />
                    </label>

                    <label class="form-control w-full max-w-xs">
                        <div class="label">
                            <span class="label-text">{{ t('dialogs.informationEdit.level') }}</span>
                        </div>
                        <input type="text" placeholder="Type here" class="input input-bordered w-full max-w-xs"
                            v-model="information.level" />
                    </label>

                    <label class="form-control w-full max-w-xs">
                        <div class="label">
                            <span class="label-text">{{ t('dialogs.informationEdit.ancestry') }}</span>
                        </div>
                        <input type="text" placeholder="Type here" class="input input-bordered w-full max-w-xs"
                            v-model="information.ancestry" />
                    </label>

                    <label class="form-control w-full max-w-xs">
                        <div class="label">
                            <span class="label-text">{{ t('dialogs.informationEdit.class') }}</span>
                        </div>
                        <input type="text" placeholder="Type here" class="input input-bordered w-full max-w-xs"
                            v-model="information.class" />
                    </label>
                </div>
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">{{ t('actions.save') }}</button>
                    <button @click="cancel" class="btn w-24" type="button">{{ t('actions.cancel') }}</button>
                </div>
            </form>
        </div>
    </dialog>

</template>

<style scoped></style>